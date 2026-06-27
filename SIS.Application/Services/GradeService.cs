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
    public class GradeService : IGradeService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IGpaCalculatorService _gpaCalculator;

        public GradeService(
            IUnitOfWork uow,
            IMapper mapper,
            IGpaCalculatorService gpaCalculator)
        {
            _uow = uow;
            _mapper = mapper;
            _gpaCalculator = gpaCalculator;
        }
        public async Task<GradeDto> CreateAsync(CreateGradeDto dto)
        {
            
            var existing = await _uow.Grades.GetByStudentAndCourseAsync(
                dto.StudentId, dto.CourseId);

            if (existing != null)
                throw new ConflictException(ErrorMessages.GradeAlreadyExists);

            var grade = _mapper.Map<Grade>(dto);

           
            var letter = _gpaCalculator.GetLetterGrade(dto.Score);
            grade.Letter = Enum.Parse<GradeLetter>(letter);

            await _uow.Grades.AddAsync(grade);
            await _uow.SaveChangesAsync();

            return _mapper.Map<GradeDto>(grade);
        }

        public async Task<IList<GradeDto>> GetByCourseIdAsync(int courseId)
        {
            var grades = await _uow.Grades.GetByCourseIdAsync(courseId);
            return _mapper.Map<IList<GradeDto>>(grades);
        }

        public async Task<IList<GradeDto>> GetByStudentIdAsync(int studentId)
        {
            var grades = await _uow.Grades.GetByStudentIdAsync(studentId);
            return _mapper.Map<IList<GradeDto>>(grades);
        }

        public async Task<GradeDto> UpdateAsync(int id, UpdateGradeDto dto)
        {
            var grade = await _uow.Grades.GetByIdAsync(id);

            if (grade == null)
                throw new NotFoundException(ErrorMessages.GradeNotFound);

            grade.Score = dto.Score;
           
            var letter = _gpaCalculator.GetLetterGrade(dto.Score);
            grade.Letter = Enum.Parse<GradeLetter>(letter);
            grade.UpdatedAt = DateTime.UtcNow;

            _uow.Grades.Update(grade);
            await _uow.SaveChangesAsync();

            return _mapper.Map<GradeDto>(grade);
        }
    }
}
