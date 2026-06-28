using SIS.Domain.Interfaces;
using StudentInformationSystem.Domain.Models;

namespace StudentInformationSystem.Domain.Interfaces
{
    public interface IStudentRepository:IGenericRepository<Student>
    {
       
        void Update(Student student);
        void Delete(Student student);
    }
}
