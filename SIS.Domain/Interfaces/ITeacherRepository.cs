using SIS.Domain.Interfaces;
using StudentInformationSystem.Domain.Models;

namespace StudentInformationSystem.Domain.Interfaces
{
    public interface ITeacherRepository:IGenericRepository<Teacher>
    {
        void Update(Teacher teacher);
        void Delete(Teacher teacher);
    }
}
