using SIS.Domain.Interfaces;
using StudentInformationSystem.Domain.Models;

namespace StudentInformationSystem.Domain.Interfaces
{
    public interface IProgrammeRepository:IGenericRepository<Programme>
    {
        void Update(Programme programme);
        void Delete(Programme programme);
    }
}
