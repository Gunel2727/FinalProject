using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIS.Application.DTOs
{
    public class AttendanceDto
    {
        public int Id { get; set; }
        public string StudentFullName { get; set; } = string.Empty;
        public string CourseName { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public string Status { get; set; } = string.Empty;
    }
    public class CreateAttendanceDto
    {
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; } = string.Empty;
    }

    public class UpdateAttendanceDto
    {
        public string Status { get; set; } = string.Empty;
    }
}
