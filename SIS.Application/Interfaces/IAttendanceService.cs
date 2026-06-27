using SIS.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIS.Application.Interfaces
{
    public interface IAttendanceService
    {
        Task<IList<AttendanceDto>> GetByStudentIdAsync(int studentId);
        Task<IList<AttendanceDto>> GetByCourseIdAsync(int courseId);
        Task<AttendanceDto> RecordAsync(CreateAttendanceDto dto);
        Task<AttendanceDto> UpdateAsync(int id, UpdateAttendanceDto dto);
    }
}
