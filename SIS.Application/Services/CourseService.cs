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
    public class CourseService : ICourseService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public CourseService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<CourseDto> CreateAsync(CreateCourseDto dto)
        {
            var course = _mapper.Map<Course>(dto);
            await _uow.Courses.AddAsync(course);
            await _uow.SaveChangesAsync();
            return _mapper.Map<CourseDto>(course);
        }

        public async Task DeleteAsync(int id)
        {
            var course = await _uow.Courses.GetByIdAsync(id);
            if (course == null)
                throw new NotFoundException(ErrorMessages.CourseNotFound);

            _uow.Courses.Delete(course);
            await _uow.SaveChangesAsync();
        }

        public async Task<IList<CourseDto>> GetAllAsync()
        {
            var courses = await _uow.Courses.GetAllAsync();
            return _mapper.Map<IList<CourseDto>>(courses);
        }

        public async Task<CourseDto> GetByIdAsync(int id)
        {
            var course = await _uow.Courses.GetByIdAsync(id);
            if (course == null)
                throw new NotFoundException(ErrorMessages.CourseNotFound);
            return _mapper.Map<CourseDto>(course);
        }

        public async Task<IList<CourseDto>> GetByTeacherIdAsync(int teacherId)
        {
            var courses = await _uow.Courses.GetByTeacherIdAsync(teacherId);
            return _mapper.Map<IList<CourseDto>>(courses);
        }

        public async Task<IList<CourseDto>> GetFilteredAsync(string? search, int? teacherId, int? academicTermId)
        {
           var courses = await _uow.Courses.GetFilteredAsync(search, teacherId, academicTermId);
            return _mapper.Map<IList<CourseDto>>(courses);
        }

        public async Task<CourseDto> UpdateAsync(int id, UpdateCourseDto dto)
        {
            var course = await _uow.Courses.GetByIdAsync(id);
            if (course == null)
                throw new NotFoundException(ErrorMessages.CourseNotFound);

            course.Name = dto.Name;
            course.Credits = dto.Credits;
            course.TeacherId = dto.TeacherId;
            course.UpdatedAt = DateTime.UtcNow;

            _uow.Courses.Update(course);
            await _uow.SaveChangesAsync();
            return _mapper.Map<CourseDto>(course);

        }
    }
}
