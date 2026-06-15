namespace StudentInformationSystem.Domain.Models
{
    public class Programme:BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public int DepartmentId { get; set; }

        public Department Department { get; set; } = null!;
        public IList<Student> Students { get; set; } = new List<Student>();
    }
}
