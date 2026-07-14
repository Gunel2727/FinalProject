using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIS.Application.DTOs
{
    public class ChartDataPointDto
    {
        public string Label { get; set; }=string.Empty;
        public int Count { get; set; }
    }
    public class CourseAverageDto
    {
        public string CourseName { get; set; } = string.Empty;
        public double AverageScore { get; set; }
    }
    public class SemesterGpaDto
    {
        public string SemesterName { get; set; } = string.Empty;
        public double Gpa { get; set; }
    }
}
