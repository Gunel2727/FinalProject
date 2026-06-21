using SIS.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIS.Application.Interfaces
{
    public interface IStudentService
    {
        Task<IList<StudentDto>> GetAllAsync();
        Task<StudentDto> GetByIdAsync(int id);
        Task<StudentDto> CreateAsync(CreateStudentDto dto);
        Task<StudentDto> UpdateAsync(int id, UpdateStudentDto dto);
        Task DeleteAsync(int id);
    }
}
