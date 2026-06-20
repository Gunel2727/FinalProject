using StudentInformationSystem.Domain.Models;

namespace StudentInformationSystem.Domain.Interfaces
{
    public interface IProgrammeRepository
    {
        Task<IList<Programme>> GetAllAsync();
        Task<Programme?> GetByIdAsync(int id);
        Task AddAsync(Programme programme);
        void Update(Programme programme);
        void Delete(Programme programme);
    }
}
