using SIS.Domain.Interfaces;
using StudentInformationSystem.Domain.Interfaces;
using StudentInformationSystem.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIS.Infrastructure.Persistence.SqlServer.Repositories
{
    public class StudentRepository : GenericRepository<Student>, IStudentRepository
    {
        public StudentRepository(AppDbContext context) : base(context)
        {
        }

        public void Delete(Student student)
        {
            _context.Students.Remove(student);
        }

        public  void Update(Student student)
        {
            _context.Students.Update(student);
        }
    }
}
