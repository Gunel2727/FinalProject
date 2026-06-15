namespace StudentInformationSystem.Domain.Models
{
    public class Department:BaseEntity
    {
        public string Name { get; set; } = string.Empty;


        public IList<Teacher> Teachers { get; set; } = new List<Teacher>();

        public IList<Programme> Programmes { get; set; } = new List<Programme>();
    }
}
