using Microsoft.EntityFrameworkCore;
using StudentInformationSystem.Domain.Interfaces;
using StudentInformationSystem.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIS.Infrastructure.Persistence.SqlServer.Repositories
{
    public class AcademicTermRepository : GenericRepository<AcademicTerm>, IAcademicTermRepository
    {
        public AcademicTermRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<AcademicTerm?> GetActiveTermAsync()
        {
            return  await _context.AcademicTerms
                .FirstOrDefaultAsync(t => t.IsActive);
        }

        public void Update(AcademicTerm term)
        {
            _context.AcademicTerms.Update(term);
        }
    }
}
