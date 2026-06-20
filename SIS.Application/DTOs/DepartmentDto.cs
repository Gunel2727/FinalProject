using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIS.Application.DTOs
{
    public class DepartmentDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }

    public class CreateDepartmentDto
    {
        public string Name { get; set; } = string.Empty;
    }

    public class UpdateDepartmentDto
    {
        public string Name { get; set; } = string.Empty;
    }
}
