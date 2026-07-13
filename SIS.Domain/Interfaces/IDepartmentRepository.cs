using SIS.Domain.Interfaces;
using StudentInformationSystem.Domain.Models;

namespace StudentInformationSystem.Domain.Interfaces
{
    public interface IDepartmentRepository:IGenericRepository<Department>
    {
       
        void Update(Department department);
        void Delete(Department department);
        Task<IList<Department>> GetFilteredAsync(string? search);
    }
}
