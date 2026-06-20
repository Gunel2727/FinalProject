using StudentInformationSystem.Domain.Models;

namespace StudentInformationSystem.Domain.Interfaces
{
    public interface IAcademicTermRepository
    {
        Task<IList<AcademicTerm>> GetAllAsync();
        Task<AcademicTerm?> GetByIdAsync(int id);
        
        Task<AcademicTerm?> GetActiveTermAsync();
        Task AddAsync(AcademicTerm term);
        void Update(AcademicTerm term);
    }
}
