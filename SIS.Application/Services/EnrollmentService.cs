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
    public class EnrollmentService : IEnrollmentService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public EnrollmentService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        public async Task<EnrollmentDto> EnrollAsync(CreateEnrollmentDto dto)
        {
            
            var existing = await _uow.Enrollments.GetByStudentAndCourseAsync(
                dto.StudentId, dto.CourseId);

            if (existing != null)
                throw new ConflictException(ErrorMessages.AlreadyEnrolled);

            var enrollment = _mapper.Map<Enrollment>(dto);
            await _uow.Enrollments.AddAsync(enrollment);
            await _uow.SaveChangesAsync();

            var enrollments = await _uow.Enrollments.GetByStudentIdAsync(dto.StudentId);
            var enrollmentWithDetails = enrollments.First(e => e.Id == enrollment.Id);

            return _mapper.Map<EnrollmentDto>(enrollmentWithDetails);
        }

        public async Task<IList<EnrollmentDto>> GetByCourseIdAsync(int courseId)
        {
            var enrollments = await _uow.Enrollments.GetByCourseIdAsync(courseId);
            var enrollmentsList = enrollments.ToList();
            var dtos = _mapper.Map<List<EnrollmentDto>>(enrollmentsList);

            foreach (var dto in dtos)
            {
                var enrollment = enrollmentsList.First(e => e.Id == dto.Id);
                var user = await _uow.Users.GetByStudentIdAsync(enrollment.StudentId);
                if (user != null) dto.StudentUserId = user.Id;
            }

            return dtos;
        }

        public async Task<IList<EnrollmentDto>> GetByStudentIdAsync(int studentId)
        {
            var enrollments = await _uow.Enrollments.GetByStudentIdAsync(studentId);
            return _mapper.Map<IList<EnrollmentDto>>(enrollments);
        }

        public async Task UnenrollAsync(int enrollmentId)
        {
            var enrollment = await _uow.Enrollments.GetByIdAsync(enrollmentId);

            if (enrollment == null)
                throw new NotFoundException(ErrorMessages.EnrollmentNotFound);

            _uow.Enrollments.Delete(enrollment);
            await _uow.SaveChangesAsync();
        }
    }
}
