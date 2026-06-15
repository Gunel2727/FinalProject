namespace StudentInformationSystem.Domain.Models
{
    public class Course:BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public int Credits { get; set; }
        public int TeacherId { get; set; }
        public int AcademicTermId { get; set; }

        public Teacher Teacher { get; set; } = null!;
        public AcademicTerm AcademicTerm { get; set; } = null!;
        
        public IList<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
    }
}
