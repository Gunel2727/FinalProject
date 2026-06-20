using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIS.Application.DTOs
{
    public class StudentDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public int AcademicYear { get; set; }
        public string ProgrammeName { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }

    public class CreateStudentDto
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public int AcademicYear { get; set; }
        public int ProgrammeId { get; set; }
    }

    
    public class UpdateStudentDto
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public int AcademicYear { get; set; }
    }
}
