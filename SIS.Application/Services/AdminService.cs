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
    public class AdminService : IAdminService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public AdminService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        public async Task<DepartmentDto> CreateDepartmentAsync(CreateDepartmentDto dto)
        {
            var department = _mapper.Map<Department>(dto);
            await _uow.Departments.AddAsync(department);
            await _uow.SaveChangesAsync();
            return _mapper.Map<DepartmentDto>(department);
        }

        public async Task<ProgrammeDto> CreateProgrammeAsync(CreateProgrammeDto dto)
        {
            var programme = _mapper.Map<Programme>(dto);
            await _uow.Programmes.AddAsync(programme);
            await _uow.SaveChangesAsync();

            var programmeWithDetails = await _uow.Programmes.GetByIdAsync(programme.Id);

            return _mapper.Map<ProgrammeDto>(programmeWithDetails);
        }

        public async Task<AcademicTermDto> CreateTermAsync(CreateAcademicTermDto dto)
        {
            var term = _mapper.Map<AcademicTerm>(dto);
            await _uow.AcademicTerms.AddAsync(term);
            await _uow.SaveChangesAsync();
            return _mapper.Map<AcademicTermDto>(term);
        }

        public async Task DeleteDepartmentAsync(int id)
        {
            var department = await _uow.Departments.GetByIdAsync(id);
            if (department == null)
                throw new NotFoundException(ErrorMessages.DepartmentNotFound);

            _uow.Departments.Delete(department);
            await _uow.SaveChangesAsync();
        }

        public async Task<IList<DepartmentDto>> GetAllDepartmentsAsync()
        {
            var departments = await _uow.Departments.GetAllAsync();
            return _mapper.Map<IList<DepartmentDto>>(departments);
        }

        public async Task<IList<ProgrammeDto>> GetAllProgrammesAsync()
        {
            var programmes = await _uow.Programmes.GetAllAsync();
            return _mapper.Map<IList<ProgrammeDto>>(programmes);
        }

        public async Task<IList<AcademicTermDto>> GetAllTermsAsync()
        {
            var terms = await _uow.AcademicTerms.GetAllAsync();
            return _mapper.Map<IList<AcademicTermDto>>(terms);
        }

        public async Task<IList<ProgrammeDto>> GetFilteredAsync(string? search, int? departmentId)
        {
            var programmes = await _uow.Programmes.GetFilteredAsync(search, departmentId);
            return _mapper.Map<IList<ProgrammeDto>>(programmes);
        }

        public async Task<IList<DepartmentDto>> GetFilteredAsync(string? search)
        {
           var departments = await _uow.Departments.GetFilteredAsync(search);
            return _mapper.Map<IList<DepartmentDto>>(departments);
        }

        public async Task<DepartmentDto> UpdateDepartmentAsync(int id, UpdateDepartmentDto dto)
        {
            var department = await _uow.Departments.GetByIdAsync(id);
            if (department == null)
                throw new NotFoundException(ErrorMessages.DepartmentNotFound);

            department.Name = dto.Name;
            department.UpdatedAt = DateTime.UtcNow;

            _uow.Departments.Update(department);
            await _uow.SaveChangesAsync();
            return _mapper.Map<DepartmentDto>(department);
        }

        public async Task<ProgrammeDto> UpdateProgrammeAsync(int id, UpdateProgrammeDto dto)
        {
            var programme = await _uow.Programmes.GetByIdAsync(id);
            if (programme == null)
                throw new NotFoundException(ErrorMessages.ProgrammeNotFound);

            programme.Name = dto.Name;
            programme.UpdatedAt = DateTime.UtcNow;

            _uow.Programmes.Update(programme);
            await _uow.SaveChangesAsync();
            return _mapper.Map<ProgrammeDto>(programme);
        }

        public async Task<AcademicTermDto> UpdateTermAsync(int id, AcademicTermDto dto)
        {
            var term = await _uow.AcademicTerms.GetByIdAsync(id);
            if (term == null)
                throw new NotFoundException(ErrorMessages.TermNotFound);

            term.Name = dto.Name;
            term.StartDate = dto.StartDate;
            term.EndDate = dto.EndDate;
            term.IsActive = dto.IsActive;
            term.UpdatedAt = DateTime.UtcNow;

            _uow.AcademicTerms.Update(term);
            await _uow.SaveChangesAsync();
            return _mapper.Map<AcademicTermDto>(term);
        }
    }
}
