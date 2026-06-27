using AutoMapper;
using SIS.Application.Common;
using SIS.Application.DTOs;
using SIS.Application.Interfaces;
using StudentInformationSystem.Domain.Interfaces;
using StudentInformationSystem.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIS.Application.Services
{
    public class StudentService : IStudentService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public StudentService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        public async Task<StudentDto> CreateAsync(CreateStudentDto dto)
        {
            
            var student = _mapper.Map<Student>(dto);
            await _uow.Students.AddAsync(student);
            await _uow.SaveChangesAsync();
            return _mapper.Map<StudentDto>(student);
        }

        public async Task DeleteAsync(int id)
        {
            var student = await _uow.Students.GetByIdAsync(id);

            if (student == null)
                throw new NotFoundException(ErrorMessages.StudentNotFound);

            _uow.Students.Delete(student);
            await _uow.SaveChangesAsync();
        }

        public async Task<IList<StudentDto>> GetAllAsync()
        {
            var students = await _uow.Students.GetAllAsync();
            return _mapper.Map<IList<StudentDto>>(students);
        }

        public async Task<StudentDto> GetByIdAsync(int id)
        {
            var student = await _uow.Students.GetByIdAsync(id);
            if (student == null)
                throw new NotFoundException(ErrorMessages.StudentNotFound);

            return _mapper.Map<StudentDto>(student);
        }

        public async Task<StudentDto> UpdateAsync(int id, UpdateStudentDto dto)
        {
            var student = await _uow.Students.GetByIdAsync(id);

            if (student == null)
                throw new NotFoundException(ErrorMessages.StudentNotFound);

            student.FirstName = dto.FirstName;
            student.LastName = dto.LastName;
            student.Email = dto.Email;
            student.Phone = dto.Phone;
            student.AcademicYear = dto.AcademicYear;
            student.UpdatedAt = DateTime.UtcNow;

            _uow.Students.Update(student);
            await _uow.SaveChangesAsync();

            return _mapper.Map<StudentDto>(student);
        }
    }
}
