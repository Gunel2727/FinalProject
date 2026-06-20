using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIS.Application.DTOs
{
    public class GradeDto
    {
        public int Id { get; set; }
        public string StudentFullName { get; set; } = string.Empty;
        public string CourseName { get; set; } = string.Empty;
        public double Score { get; set; }
        public string Letter { get; set; } = string.Empty;
        public DateTime GradedAt { get; set; }
    }

   
    public class CreateGradeDto
    {
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public double Score { get; set; }
    }

    public class UpdateGradeDto
    {
        public double Score { get; set; }
    }
}
