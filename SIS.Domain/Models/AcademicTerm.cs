namespace StudentInformationSystem.Domain.Models
{
    public class AcademicTerm:BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }

        public ICollection<Course> Courses { get; set; } = new List<Course>();
    }
}
