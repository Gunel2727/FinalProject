namespace StudentInformationSystem.Domain.Models
{
    public class Student
    {
        public Guid Id { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public int AcademicYear { get; set; }

        public Guid ProgrammeId { get; set; }

        public Programme Programme { get; set; }
    }
}
