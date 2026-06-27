using SIS.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIS.Application.Interfaces
{
    public interface IAdminService
    {
        
        Task<IList<DepartmentDto>> GetAllDepartmentsAsync();
        Task<DepartmentDto> CreateDepartmentAsync(CreateDepartmentDto dto);
        Task<DepartmentDto> UpdateDepartmentAsync(int id, UpdateDepartmentDto dto);
        Task DeleteDepartmentAsync(int id);

       
        Task<IList<ProgrammeDto>> GetAllProgrammesAsync();
        Task<ProgrammeDto> CreateProgrammeAsync(CreateProgrammeDto dto);
        Task<ProgrammeDto> UpdateProgrammeAsync(int id, UpdateProgrammeDto dto);

       
        Task<IList<AcademicTermDto>> GetAllTermsAsync();
        Task<AcademicTermDto> CreateTermAsync(CreateAcademicTermDto dto);
        Task<AcademicTermDto> UpdateTermAsync(int id, AcademicTermDto dto);
    }
}
