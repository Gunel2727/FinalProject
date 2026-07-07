using SIS.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIS.Application.Interfaces
{
    public interface IDashboardService
    {
        Task<StudentDashboardDto> GetStudentDashboardAsync(int studentId);
        Task<TeacherDashboardDto> GetTeacherDashboardAsync(int teacherId);
        Task<AdminDashboardDto> GetAdminDashboardAsync();
        Task<AdvisorStudentOverviewDto> GetAdvisorOverviewAsync(int studentId);
    }
}
