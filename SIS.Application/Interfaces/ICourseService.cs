using SIS.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIS.Application.Interfaces
{
    public interface ICourseService
    {
        Task<IList<CourseDto>> GetAllAsync();
        Task<CourseDto> GetByIdAsync(int id);
        Task<IList<CourseDto>> GetByTeacherIdAsync(int teacherId);
        Task<CourseDto> CreateAsync(CreateCourseDto dto);
        Task<CourseDto> UpdateAsync(int id, UpdateCourseDto dto);
        Task DeleteAsync(int id);
    }
}
