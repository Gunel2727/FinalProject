using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIS.Application.DTOs
{
    public class CourseDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public int Credits { get; set; }
        
        public string TeacherFullName { get; set; } = string.Empty;
        public string AcademicTermName { get; set; } = string.Empty;
    }

    public class CreateCourseDto
    {
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public int Credits { get; set; }
        public int TeacherId { get; set; }
        public int AcademicTermId { get; set; }
    }

    public class UpdateCourseDto
    {
        public string Name { get; set; } = string.Empty;
        public int Credits { get; set; }
        public int TeacherId { get; set; }
    }
}
