using SIS.Domain.Interfaces;
using StudentInformationSystem.Domain.Models;

namespace StudentInformationSystem.Domain.Interfaces
{
    public interface IAcademicTermRepository:IGenericRepository<AcademicTerm>
    {
        Task<AcademicTerm?> GetActiveTermAsync();
        void Update(AcademicTerm term);
    }
}
