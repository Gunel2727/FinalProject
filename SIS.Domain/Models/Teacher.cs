namespace StudentInformationSystem.Domain.Models
{
    public class Teacher: BaseEntity
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int DepartmentId { get; set; }

        public Department Department { get; set; } = null!;
       
        public ICollection<Course> Courses { get; set; } = new List<Course>();
    }
}
