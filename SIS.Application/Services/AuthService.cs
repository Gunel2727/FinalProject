using SIS.Application.Common;
using SIS.Application.DTOs;
using SIS.Application.Interfaces;
using SIS.Domain.Models;
using StudentInformationSystem.Domain.Enums;
using StudentInformationSystem.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SIS.Application.DTOs.AuthDto;

namespace SIS.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _uow;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtTokenService _jwtTokenService;

        public AuthService(
            IUnitOfWork uow,
            IPasswordHasher passwordHasher,
            IJwtTokenService jwtTokenService)
        {
            _uow = uow;
            _passwordHasher = passwordHasher;
            _jwtTokenService = jwtTokenService;
        }

        public async Task<AuthResponseDto> LoginAsync(LoginDto dto)
        {
            var user = await _uow.Users.GetByEmailAsync(dto.Email);

            if (user==null || !_passwordHasher.Verify(dto.Password,user.PasswordHash))
            {
                throw new UnauthorizedException(ErrorMessages.InvalidCredentials);
            }

            var token = _jwtTokenService.GenerateToken(user);

            return new AuthResponseDto
            {
                Token = token,
                Email = user.Email,
                Role = user.Role.ToString()
            };
        }

        public async Task<AuthResponseDto> RegisterAsync(RegisterDto dto)
        {
           var exists= await _uow.Users.EmailExistsAsync(dto.Email);

            if(exists)
            {
                throw new ConflictException(ErrorMessages.EmailAlreadyExists);
            }

            var role = Enum.Parse<UserRole>(dto.Role, ignoreCase: true);

            if (role == UserRole.Teacher)
            {
                if (dto.TeacherId == null)
                    throw new BadRequestException("Teacher rolü üçün TeacherId mütləqdir");

                var teacher = await _uow.Teachers.GetByIdAsync(dto.TeacherId.Value);
                if (teacher == null)
                    throw new NotFoundException(ErrorMessages.TeacherNotFound);

                var alreadyLinked = await _uow.Users.TeacherIdExistsAsync(dto.TeacherId.Value);
                if (alreadyLinked)
                    throw new ConflictException("Bu müəllim üçün artıq hesab yaradılıb");
            }

            if (role == UserRole.Student)
            {
                if (dto.StudentId == null)
                    throw new BadRequestException("Student rolü üçün StudentId mütləqdir");

                var student = await _uow.Students.GetByIdAsync(dto.StudentId.Value);
                if (student == null)
                    throw new NotFoundException(ErrorMessages.StudentNotFound);

                var alreadyLinked = await _uow.Users.StudentIdExistsAsync(dto.StudentId.Value);
                if (alreadyLinked)
                    throw new ConflictException("Bu tələbə üçün artıq hesab yaradılıb");
            }



            var user = new User
            {
                Email = dto.Email,
                PasswordHash = _passwordHasher.Hash(dto.Password),
                Role = role,
                StudentId = dto.StudentId,
                TeacherId = dto.TeacherId
            };

            await _uow.Users.AddAsync(user);
            await _uow.SaveChangesAsync();

          
            var token = _jwtTokenService.GenerateToken(user);

            return new AuthResponseDto
            {
                Token = token,
                Email = user.Email,
                Role = user.Role.ToString()
            };
        }
    }
}
