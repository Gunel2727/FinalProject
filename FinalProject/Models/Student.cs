namespace StudentInformationSystem.Domain.Models
{
    public class Student:BaseEntity
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public int AcademicYear { get; set; }
        public int ProgrammeId { get; set; }

       
        public Programme Programme { get; set; } = null!;
        public IList<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
        public IList<Grade> Grades { get; set; } = new List<Grade>();
    }
}
