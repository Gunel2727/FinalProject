using AutoMapper;
using SIS.Application.Common;
using SIS.Application.DTOs;
using SIS.Application.Interfaces;
using StudentInformationSystem.Domain.Enums;
using StudentInformationSystem.Domain.Interfaces;
using StudentInformationSystem.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIS.Application.Services
{
    public class AnnouncementService : IAnnouncementService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public AnnouncementService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        public async Task<AnnouncementDto> CreateAsync(CreateAnnouncementDto dto)
        {
            var announcement = _mapper.Map<Announcement>(dto);
            if (!string.IsNullOrEmpty(dto.TargetRole))
                announcement.TargetRole = Enum.Parse<UserRole>(dto.TargetRole, ignoreCase: true);
            await _uow.Announcements.AddAsync(announcement);
            await _uow.SaveChangesAsync();
            return _mapper.Map<AnnouncementDto>(announcement);
        }

        public async Task DeleteAsync(int id)
        {
            var announcement = await _uow.Announcements.GetByIdAsync(id);

            if (announcement == null)
                throw new NotFoundException(ErrorMessages.AnnouncementNotFound);

            _uow.Announcements.Delete(announcement);
            await _uow.SaveChangesAsync();
        }

        public async Task<IList<AnnouncementDto>> GetAllAsync()
        {
            var announcements = await _uow.Announcements.GetAllAsync();
            return _mapper.Map<IList<AnnouncementDto>>(announcements);
        }

        public async Task<IList<AnnouncementDto>> GetByCourseIdAsync(int courseId)
        {
            var announcements = await _uow.Announcements.GetByCourseIdAsync(courseId);
            return _mapper.Map<IList<AnnouncementDto>>(announcements);
        }

        public async Task<IList<AnnouncementDto>> GetFilteredAsync(string? targetRole, int? courseId)
        {
            var announcements = await _uow.Announcements.GetFilteredAsync(targetRole, courseId);
            return _mapper.Map<IList<AnnouncementDto>>(announcements);
        }

        public async Task<IList<AnnouncementDto>> GetVisibleForRoleAsync(string? role)
        {
            var announcements = await _uow.Announcements.GetVisibleForRoleAsync(role);
            return _mapper.Map<IList<AnnouncementDto>>(announcements);
        }
    }
}
