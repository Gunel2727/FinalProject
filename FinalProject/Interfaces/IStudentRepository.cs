using StudentInformationSystem.Domain.Models;

namespace StudentInformationSystem.Domain.Interfaces
{
    public interface IStudentRepository
    {
        Task<Student?> GetByIdAsync(int id);
        Task<IList<Student>> GetAllAsync();
        Task AddAsync(Student student);
        void Update(Student student);
        void Delete(Student student);
    }
}
