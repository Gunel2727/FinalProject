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
    public class EnrollmentService : IEnrollmentService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public EnrollmentService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        public Task<EnrollmentDto> EnrollAsync(CreateEnrollmentDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<IList<EnrollmentDto>> GetByStudentIdAsync(int studentId)
        {
            throw new NotImplementedException();
        }

        public Task UnenrollAsync(int enrollmentId)
        {
            throw new NotImplementedException();
        }
    }
}
