using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIS.Application.DTOs
{
    public class AnnouncementDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public bool IsGlobal { get; set; }
        public int? CourseId { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class CreateAnnouncementDto
    {
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public bool IsGlobal { get; set; }
        public int? CourseId { get; set; }
    }
}
