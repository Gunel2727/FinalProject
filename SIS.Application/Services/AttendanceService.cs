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
    public class AttendanceService : IAttendanceService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public AttendanceService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        public async Task<IList<AttendanceDto>> GetByCourseIdAsync(int courseId)
        {
            var records = await _uow.Attendances.GetByCourseIdAsync(courseId);
            return _mapper.Map<IList<AttendanceDto>>(records);
        }

        public async Task<IList<AttendanceDto>> GetByStudentIdAsync(int studentId)
        {
            var records = await _uow.Attendances.GetByStudentIdAsync(studentId);
            return _mapper.Map<IList<AttendanceDto>>(records);
        }

        public async Task<AttendanceDto> RecordAsync(CreateAttendanceDto dto)
        {
            
            var existing = await _uow.Attendances.GetByCourseAndDateAsync(
                dto.CourseId, dto.Date);

            var alreadyRecorded = existing.Any(a => a.StudentId == dto.StudentId);
            if (alreadyRecorded)
                throw new ConflictException(ErrorMessages.AttendanceAlreadyRecorded);

            var attendance = new Attendance
            {
                StudentId = dto.StudentId,
                CourseId = dto.CourseId,
                Date = dto.Date,
                Status = Enum.Parse<AttendanceStatus>(dto.Status, ignoreCase: true)
            };

            await _uow.Attendances.AddAsync(attendance);
            await _uow.SaveChangesAsync();

            var attendancesForStudent = await _uow.Attendances.GetByStudentIdAsync(dto.StudentId);
            var attendanceWithDetails = attendancesForStudent.First(a => a.Id == attendance.Id);

            return _mapper.Map<AttendanceDto>(attendanceWithDetails);
        }

        public async Task<AttendanceDto> UpdateAsync(int id, UpdateAttendanceDto dto)
        {
            var attendance = await _uow.Attendances.GetByIdAsync(id);

            if (attendance == null)
                throw new NotFoundException(ErrorMessages.AttendanceNotFound);

            attendance.Status = Enum.Parse<AttendanceStatus>(dto.Status, ignoreCase: true);
            attendance.UpdatedAt = DateTime.UtcNow;

            _uow.Attendances.Update(attendance);
            await _uow.SaveChangesAsync();

            return _mapper.Map<AttendanceDto>(attendance);
        }
    }
}
