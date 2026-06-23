using SIS.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIS.Application.Interfaces
{
    public interface ITeacherService
    {
        Task<IList<TeacherDto>> GetAllAsync();
        Task<TeacherDto> GetByIdAsync(int id);
        Task<TeacherDto> CreateAsync(CreateTeacherDto dto);
        Task<TeacherDto> UpdateAsync(int id, UpdateTeacherDto dto);
        Task DeleteAsync(int id);
    }
}
