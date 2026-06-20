using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIS.Application.DTOs
{
    public class ProgrammeDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string DepartmentName { get; set; } = string.Empty;
    }

    public class CreateProgrammeDto
    {
        public string Name { get; set; } = string.Empty;
        public int DepartmentId { get; set; }
    }

    public class UpdateProgrammeDto
    {
        public string Name { get; set; } = string.Empty;
    }
}
