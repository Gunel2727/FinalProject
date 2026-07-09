using Microsoft.EntityFrameworkCore;
using SIS.Domain.Models;
using StudentInformationSystem.Domain.Enums;
using StudentInformationSystem.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIS.Infrastructure.Persistence.SqlServer
{
    public static class DataSeeder
    {
        public static async Task SeedAsync(AppDbContext context)
        {
           
            if (await context.Departments.AnyAsync())
                return;

      
            var department = new Department
            {
                Name = "Kompüter Elmləri"
            };
            context.Departments.Add(department);
            await context.SaveChangesAsync();

           
            var programme = new Programme
            {
                Name = "Software Engineering",
                DepartmentId = department.Id
            };
            context.Programmes.Add(programme);
            await context.SaveChangesAsync();

           
            var term = new AcademicTerm
            {
                Name = "2025-2026 Payız",
                StartDate = new DateTime(2025, 9, 1),
                EndDate = new DateTime(2026, 1, 31),
                IsActive = true
            };
            context.AcademicTerms.Add(term);
            await context.SaveChangesAsync();

            
            var teacher = new Teacher
            {
                FirstName = "Nicat",
                LastName = "Sirinov",
                Email = "nicat.sirinov@sis.edu.az",
                DepartmentId = department.Id
            };
            context.Teachers.Add(teacher);
            await context.SaveChangesAsync();

          
            var student = new Student
            {
                FirstName = "Gunel",
                LastName = "Musazade",
                Email = "gunelmusazade03@gmail.com",
                Phone = "0501234567",
                DateOfBirth = new DateTime(2003, 5, 15),
                AcademicYear = 2,
                ProgrammeId = programme.Id
            };
            context.Students.Add(student);
            await context.SaveChangesAsync();

           
            var course = new Course
            {
                Name = "Verilənlər Bazası",
                Code = "CS301",
                Credits = 4,
                TeacherId = teacher.Id,
                AcademicTermId = term.Id
            };
            context.Courses.Add(course);
            await context.SaveChangesAsync();

           
            var enrollment = new Enrollment
            {
                StudentId = student.Id,
                CourseId = course.Id
            };
            context.Enrollments.Add(enrollment);
            await context.SaveChangesAsync();

           
            var grade = new Grade
            {
                StudentId = student.Id,
                CourseId = course.Id,
                Score = 88,
                Letter = GradeLetter.B
            };
            context.Grades.Add(grade);
            await context.SaveChangesAsync();

            var adminUser = new User
            {
                Email = "admin@sis.edu.az",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin123!"),
                Role = UserRole.Admin
            };

            var teacherUser = new User
            {
                Email = teacher.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("Teacher123!"),
                Role = UserRole.Teacher,
                TeacherId = teacher.Id
            };

            var studentUser = new User
            {
                Email = student.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("Student123!"),
                Role = UserRole.Student,
                StudentId = student.Id
            };

            context.Users.AddRange(adminUser, teacherUser, studentUser);
            await context.SaveChangesAsync();
        }
    }
}
