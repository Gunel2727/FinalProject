using Microsoft.Extensions.Options;
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
        private readonly IEmailService _emailService;
        private readonly IGoogleAuthService _googleAuthService;

        public AuthService(
            IUnitOfWork uow,
            IPasswordHasher passwordHasher,
            IJwtTokenService jwtTokenService,
            IEmailService emailService,
            IGoogleAuthService googleAuthService

            )
        {
            _uow = uow;
            _passwordHasher = passwordHasher;
            _jwtTokenService = jwtTokenService;
            _emailService = emailService;
            _googleAuthService = googleAuthService;
        }

        public async Task<bool> ForgotPasswordAsync(ForgotPasswordDto dto)
        {
            var user = await _uow.Users.GetByEmailAsync(dto.Email);

            if(user==null)
                return false;


            var newToken = Guid.NewGuid().ToString();
            user.ResetTokenExpiry=DateTime.UtcNow.AddMinutes(15);
            user.ResetToken = newToken;

            _uow.Users.Update(user);
            await _uow.SaveChangesAsync();

            await _emailService.SendPasswordResetEmailAsync(user.Email, newToken);
            return true;
            
        }

        public async Task<AuthResponseDto> GoogleLoginAsync(GoogleLoginDto dto)
        {
            var email = await _googleAuthService.ValidateTokenAndGetEmailAsync(dto.IdToken);

            if (email == null)
                throw new UnauthorizedException("Google token doğrulanmadı");

            var user = await _uow.Users.GetByEmailAsync(email);
            if (user == null)
                throw new UnauthorizedException(
                     "Bu Google hesabı ilə sistemdə qeydiyyat yoxdur.");

            var token = _jwtTokenService.GenerateToken(user);

            return new AuthResponseDto
            {
                Token = token,
                Email = user.Email,
                Role = user.Role.ToString(),
                UserId = user.Id,
                StudentId = user.StudentId,
                TeacherId = user.TeacherId
            };

        }

        public async Task<AuthResponseDto> LoginAsync(LoginDto dto)
        {
            var user = await _uow.Users.GetByEmailAsync(dto.Email);

            if (user == null)
                throw new UnauthorizedException(ErrorMessages.InvalidCredentials);

            if (user.IsGoogleAccount || user.PasswordHash == null)
                throw new BadRequestException("Bu hesab Google ilə yaradılıb. Zəhmət olmasa 'Google ilə daxil ol' düyməsini işlədin.");

            if (!_passwordHasher.Verify(dto.Password, user.PasswordHash))
                throw new UnauthorizedException(ErrorMessages.InvalidCredentials);

            var token = _jwtTokenService.GenerateToken(user);

            return new AuthResponseDto
            {
                Token = token,
                Email = user.Email,
                Role = user.Role.ToString(),
                UserId = user.Id,
                StudentId = user.StudentId,
                TeacherId = user.TeacherId
            };
        }

        public async Task<AuthResponseDto> RegisterAsync(RegisterDto dto)
        {
            var exists = await _uow.Users.EmailExistsAsync(dto.Email);
            if (exists)
                throw new ConflictException(ErrorMessages.EmailAlreadyExists);

            var role = Enum.Parse<UserRole>(dto.Role, ignoreCase: true);

            
            int? studentId = null;
            int? teacherId = null;

            if (role == UserRole.Teacher)
            {
                var teacher = await _uow.Teachers.GetByEmailAsync(dto.Email);
                if (teacher == null)
                    throw new NotFoundException(
                        "Bu email ilə qeydə alınmış müəllim tapılmadı. Əvvəlcə Admin sizi sistemə əlavə etməlidir.");

                var alreadyLinked = await _uow.Users.TeacherIdExistsAsync(teacher.Id);
                if (alreadyLinked)
                    throw new ConflictException("Bu müəllim üçün artıq hesab yaradılıb");

                teacherId = teacher.Id;
            }

            if (role == UserRole.Student)
            {
                var student = await _uow.Students.GetByEmailAsync(dto.Email);
                if (student == null)
                    throw new NotFoundException(
                        "Bu email ilə qeydə alınmış tələbə tapılmadı. Əvvəlcə Admin sizi sistemə əlavə etməlidir.");

                var alreadyLinked = await _uow.Users.StudentIdExistsAsync(student.Id);
                if (alreadyLinked)
                    throw new ConflictException("Bu tələbə üçün artıq hesab yaradılıb");

                studentId = student.Id;
            }

            var user = new User
            {
                Email = dto.Email,
                PasswordHash = _passwordHasher.Hash(dto.Password),
                Role = role,
                StudentId = studentId,  
                TeacherId = teacherId   
            };

            await _uow.Users.AddAsync(user);
            await _uow.SaveChangesAsync();

            var token = _jwtTokenService.GenerateToken(user);

            return new AuthResponseDto
            {
                Token = token,
                Email = user.Email,
                Role = user.Role.ToString(),
                UserId = user.Id,
                StudentId = user.StudentId,
                TeacherId = user.TeacherId
            };
        }

        public async Task<bool> ResetPasswordAsync(ResetPasswordDto dto)
        {
           var user = await _uow.Users.GetByEmailAsync(dto.Email);

            if(user==null)
            {
                throw new NotFoundException(ErrorMessages.UserNotFound);
            }

            if(user.ResetToken != dto.ResetToken)
            {
                throw new BadRequestException("Reset token yanlışdır");
            }

            if (user.ResetTokenExpiry < DateTime.UtcNow)
            {
                throw new BadRequestException("Reset token  vaxtı keçib");
            }

            user.PasswordHash = _passwordHasher.Hash(dto.NewPassword);  
            user.ResetToken = null;
            user.ResetTokenExpiry = null;

            _uow.Users.Update(user);
           await  _uow.SaveChangesAsync();

            return true;



        }
    }
}
