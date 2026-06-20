using StudentInformationSystem.Domain.Models;

namespace StudentInformationSystem.Domain.Interfaces
{
    public interface IDepartmentRepository
    {
        Task<IList<Department>> GetAllAsync();
        Task<Department?> GetByIdAsync(int id);
        Task AddAsync(Department department);
        void Update(Department department);
        void Delete(Department department);
    }
}
