using SIS.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIS.Application.Interfaces
{
    public interface IGradeService
    {
        Task<IList<GradeDto>> GetByStudentIdAsync(int studentId);
        Task<IList<GradeDto>> GetByCourseIdAsync(int courseId);
        Task<GradeDto> CreateAsync(CreateGradeDto dto);
        Task<GradeDto> UpdateAsync(int id, UpdateGradeDto dto);
        Task<IList<GradeDto>> GetFilteredAsync(string? search, int? courseId);
    }
}
