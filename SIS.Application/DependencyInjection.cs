using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;
using SIS.Application.Interfaces;
using SIS.Application.Profiles;
using SIS.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIS.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(
       this IServiceCollection services)
        {

            services.AddAutoMapper(cfg => cfg.AddProfile<MapperProfile>());


            services.AddValidatorsFromAssembly(
                typeof(DependencyInjection).Assembly);


            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<ICourseService, CourseService>();
            services.AddScoped<IGradeService, GradeService>();
            services.AddScoped<IEnrollmentService, EnrollmentService>();
            services.AddScoped<IAttendanceService, AttendanceService>();
            services.AddScoped<IAnnouncementService, AnnouncementService>();
            services.AddScoped<IDashboardService, DashboardService>();
            services.AddScoped<IGpaCalculatorService, GpaCalculatorService>();
            services.AddScoped<IAdminService, AdminService>();
            services.AddScoped<ITeacherService, TeacherService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IMessageService, MessageService>();

            return services;
        }
    }
}
