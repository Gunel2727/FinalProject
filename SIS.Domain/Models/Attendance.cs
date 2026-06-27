using StudentInformationSystem.Domain.Enums;

namespace StudentInformationSystem.Domain.Models
{
    public class Attendance:BaseEntity
    {
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public DateTime Date { get; set; }
        public AttendanceStatus Status { get; set; }
        public Student Student { get; set; } = null!;
        public Course Course { get; set; } = null!;
    }
}
