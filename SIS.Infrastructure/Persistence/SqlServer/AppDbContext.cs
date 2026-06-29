using Microsoft.EntityFrameworkCore;
using StudentInformationSystem.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIS.Infrastructure.Persistence.SqlServer
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

       
        public DbSet<Student> Students => Set<Student>();
        public DbSet<Teacher> Teachers => Set<Teacher>();
        public DbSet<Course> Courses => Set<Course>();
        public DbSet<Enrollment> Enrollments => Set<Enrollment>();
        public DbSet<Grade> Grades => Set<Grade>();
        public DbSet<Attendance> Attendances => Set<Attendance>();
        public DbSet<Announcement> Announcements => Set<Announcement>();
        public DbSet<Department> Departments => Set<Department>();
        public DbSet<Programme> Programmes => Set<Programme>();
        public DbSet<AcademicTerm> AcademicTerms => Set<AcademicTerm>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(
                typeof(AppDbContext).Assembly);
        }
    }
}
