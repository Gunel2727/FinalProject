namespace StudentInformationSystem.Domain.Models
{
    public class Announcement:BaseEntity
    {
        
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        
        public bool IsGlobal { get; set; }
       
        public int? CourseId { get; set; }
       
    }
}
