using StudentInformationSystem.Domain.Models;

namespace StudentInformationSystem.Domain.Interfaces
{
    public interface ITeacherRepository
    {
        Task<Teacher?> GetByIdAsync(int id);
        Task<IEnumerable<Teacher>> GetAllAsync();
        Task AddAsync(Teacher teacher);
        void Update(Teacher teacher);
        void Delete(Teacher teacher);
    }
}
