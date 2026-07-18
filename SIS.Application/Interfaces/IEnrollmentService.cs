using SIS.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIS.Application.Interfaces
{
    public interface IEnrollmentService
    {
        Task<IList<EnrollmentDto>> GetByStudentIdAsync(int studentId);
        Task<EnrollmentDto> EnrollAsync(CreateEnrollmentDto dto);
        Task UnenrollAsync(int enrollmentId);
        Task<IList<EnrollmentDto>> GetByCourseIdAsync(int courseId);
    }
}
