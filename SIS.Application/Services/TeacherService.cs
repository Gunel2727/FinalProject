using AutoMapper;
using SIS.Application.Common;
using SIS.Application.DTOs;
using SIS.Application.Interfaces;
using SIS.Domain.Models;
using StudentInformationSystem.Domain.Enums;
using StudentInformationSystem.Domain.Interfaces;
using StudentInformationSystem.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIS.Application.Services
{
    public class TeacherService : ITeacherService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IEmailService _emailService;

        public TeacherService(IUnitOfWork uow, IMapper mapper, IPasswordHasher passwordHasher, IEmailService emailService)
        {
            _uow = uow;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
            _emailService = emailService;
        }

        public async Task<TeacherDto> CreateAsync(CreateTeacherDto dto)
        {
            var emailExists = await _uow.Users.EmailExistsAsync(dto.Email);
            if (emailExists)
                throw new ConflictException(ErrorMessages.EmailAlreadyExists);

            var teacher = _mapper.Map<Teacher>(dto);
            await _uow.Teachers.AddAsync(teacher);
            await _uow.SaveChangesAsync();

            var tempPassword = PasswordGenerator.Generate();
            var user = new User
            {
                Email = teacher.Email,
                PasswordHash = _passwordHasher.Hash(tempPassword),
                Role = UserRole.Teacher,
                TeacherId = teacher.Id,
                MustChangePassword=true
            };
            await _uow.Users.AddAsync(user);
            await _uow.SaveChangesAsync();

            await _emailService.SendWelcomeEmailAsync(
           teacher.Email,
           $"{teacher.FirstName} {teacher.LastName}",
           tempPassword);

            var resultDto = _mapper.Map<TeacherDto>(teacher);
            resultDto.TemporaryPassword = tempPassword;
            return resultDto;
        }

        public async Task DeleteAsync(int id)
        {
            var teacher = await _uow.Teachers.GetByIdAsync(id);
            if (teacher == null)
                throw new NotFoundException(ErrorMessages.TeacherNotFound);

            _uow.Teachers.Delete(teacher);
            await _uow.SaveChangesAsync();
        }

        public async Task<IList<TeacherDto>> GetAllAsync()
        {
            var teachers = await _uow.Teachers.GetAllAsync();
            return _mapper.Map<IList<TeacherDto>>(teachers);
        }

        public async Task<TeacherDto> GetByIdAsync(int id)
        {
            var teacher = await _uow.Teachers.GetByIdAsync(id);
            if (teacher == null)
                throw new NotFoundException(ErrorMessages.TeacherNotFound);
            return _mapper.Map<TeacherDto>(teacher);
        }

        public async Task<TeacherDto> UpdateAsync(int id, UpdateTeacherDto dto)
        {
            var teacher = await _uow.Teachers.GetByIdAsync(id);
            if (teacher == null)
                throw new NotFoundException(ErrorMessages.TeacherNotFound);

            teacher.FirstName = dto.FirstName;
            teacher.LastName = dto.LastName;
            teacher.DepartmentId = dto.DepartmentId;
            teacher.UpdatedAt = DateTime.UtcNow;

            _uow.Teachers.Update(teacher);
            await _uow.SaveChangesAsync();
            return _mapper.Map<TeacherDto>(teacher);
        }
    }
}
