using SIS.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIS.Application.Interfaces
{
    public interface IAnnouncementService
    {
        Task<IList<AnnouncementDto>> GetAllAsync();
        Task<IList<AnnouncementDto>> GetByCourseIdAsync(int courseId);
        Task<AnnouncementDto> CreateAsync(CreateAnnouncementDto dto);
        Task DeleteAsync(int id);
        Task<IList<AnnouncementDto>> GetFilteredAsync(string? targetRole, int? courseId);
    }
}
