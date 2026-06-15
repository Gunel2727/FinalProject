using StudentInformationSystem.Domain.Enums;

namespace StudentInformationSystem.Domain.Models
{
    public class Grade:BaseEntity
    {
        public int StudentId { get; set; }
        public int CourseId { get; set; }
       
        public double Score { get; set; }
       
        public GradeLetter Letter { get; set; }

        public Student Student { get; set; } = null!;
        public Course Course { get; set; } = null!;
    }
}
