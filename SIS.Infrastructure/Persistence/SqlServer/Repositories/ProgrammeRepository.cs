using StudentInformationSystem.Domain.Interfaces;
using StudentInformationSystem.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIS.Infrastructure.Persistence.SqlServer.Repositories
{
    public class ProgrammeRepository : GenericRepository<Programme>, IProgrammeRepository
    {
        public ProgrammeRepository(AppDbContext context) : base(context)
        {
        }

        public void Delete(Programme programme)
        {
            _context.Programmes.Remove(programme);
        }

        public void Update(Programme programme)
        {
            _context.Programmes.Update(programme);
        }
    }
}
