using AutoMapper;
using SIS.Application.DTOs;
using SIS.Application.Interfaces;
using StudentInformationSystem.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIS.Application.Services
{
    public class AdminService : IAdminService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public AdminService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        public Task<DepartmentDto> CreateDepartmentAsync(CreateDepartmentDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<ProgrammeDto> CreateProgrammeAsync(CreateProgrammeDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<AcademicTermDto> CreateTermAsync(CreateAcademicTermDto dto)
        {
            throw new NotImplementedException();
        }

        public Task DeleteDepartmentAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IList<DepartmentDto>> GetAllDepartmentsAsync()
        {
            var departments = await _uow.Departments.GetAllAsync();
            return _mapper.Map<IList<DepartmentDto>>(departments);
        }

        public Task<IList<ProgrammeDto>> GetAllProgrammesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IList<AcademicTermDto>> GetAllTermsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<DepartmentDto> UpdateDepartmentAsync(int id, UpdateDepartmentDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<ProgrammeDto> UpdateProgrammeAsync(int id, UpdateProgrammeDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<AcademicTermDto> UpdateTermAsync(int id, AcademicTermDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
