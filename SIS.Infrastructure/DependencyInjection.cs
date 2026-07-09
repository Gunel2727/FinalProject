using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QuestPDF.Infrastructure;
using SIS.Application.Interfaces;
using SIS.Infrastructure.ExternalServices;
using SIS.Infrastructure.Identity;
using SIS.Infrastructure.Persistence.SqlServer;
using StudentInformationSystem.Domain.Interfaces;
using StudentInformationSystem.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIS.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
       this IServiceCollection services,
       IConfiguration configuration)
        {
            QuestPDF.Settings.License = LicenseType.Community;


            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection")));

            
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.Configure<JwtSettings>(
           configuration.GetSection("JwtSettings"));

            services.Configure<EmailSettings>(
                configuration.GetSection("EmailSettings"));

            services.Configure<GoogleAuthSettings>(
                configuration.GetSection("GoogleAuthSettings"));

            services.AddScoped<IJwtTokenService, JwtTokenService>();
            services.AddScoped<IPasswordHasher, PasswordHasher>();
            services.AddScoped<ITranscriptService, TranscriptService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IGoogleAuthService, GoogleAuthService>();

            return services;
        }
    }
}
