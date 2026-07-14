using AutoMapper;
using SIS.Application.DTOs;
using SIS.Domain.Models;
using StudentInformationSystem.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIS.Application.Profiles
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Student, StudentDto>()
           .ForMember(
               dest => dest.ProgrammeName,
               opt => opt.MapFrom(src => src.Programme.Name));

            CreateMap<CreateStudentDto, Student>();

            CreateMap<UpdateStudentDto, Student>();

            CreateMap<Teacher, TeacherDto>()
           .ForMember(
               dest => dest.DepartmentName,
               opt => opt.MapFrom(src => src.Department.Name));

            CreateMap<CreateTeacherDto, Teacher>();
            CreateMap<UpdateTeacherDto, Teacher>();

            CreateMap<Course, CourseDto>()
          .ForMember(
              dest => dest.TeacherFullName,
              opt => opt.MapFrom(src =>
                  $"{src.Teacher.FirstName} {src.Teacher.LastName}"))
          .ForMember(
              dest => dest.AcademicTermName,
              opt => opt.MapFrom(src => src.AcademicTerm.Name));

            CreateMap<CreateCourseDto, Course>();
            CreateMap<UpdateCourseDto, Course>();

            CreateMap<Enrollment, EnrollmentDto>()
           .ForMember(
               dest => dest.StudentFullName,
               opt => opt.MapFrom(src =>
                   $"{src.Student.FirstName} {src.Student.LastName}"))
           .ForMember(
               dest => dest.CourseName,
               opt => opt.MapFrom(src => src.Course.Name))
           .ForMember(
               dest => dest.CourseCode,
               opt => opt.MapFrom(src => src.Course.Code));


            CreateMap<CreateEnrollmentDto, Enrollment>();

            CreateMap<Grade, GradeDto>()
           .ForMember(
               dest => dest.StudentFullName,
               opt => opt.MapFrom(src =>
                   $"{src.Student.FirstName} {src.Student.LastName}"))
           .ForMember(
               dest => dest.CourseName,
               opt => opt.MapFrom(src => src.Course.Name))
           .ForMember(
               dest => dest.Letter,
               opt => opt.MapFrom(src => src.Letter.ToString()))
           .ForMember(
               dest => dest.GradedAt,
               opt => opt.MapFrom(src => src.CreatedAt));

            CreateMap<CreateGradeDto, Grade>();
            CreateMap<UpdateGradeDto, Grade>();

            CreateMap<Attendance, AttendanceDto>()
           .ForMember(
               dest => dest.StudentFullName,
               opt => opt.MapFrom(src =>
                   $"{src.Student.FirstName} {src.Student.LastName}"))
           .ForMember(
               dest => dest.CourseName,
               opt => opt.MapFrom(src => src.Course.Name))
           .ForMember(
               dest => dest.Status,
               opt => opt.MapFrom(src => src.Status.ToString()));

            CreateMap<CreateAttendanceDto, Attendance>();
            CreateMap<UpdateAttendanceDto, Attendance>();

            CreateMap<Announcement, AnnouncementDto>()
            .ForMember(d => d.TargetRole, 
            o => o.MapFrom(s => s.TargetRole.HasValue ? s.TargetRole.Value.ToString() : null));

            CreateMap<CreateAnnouncementDto, Announcement>();

            CreateMap<Department, DepartmentDto>();
            CreateMap<CreateDepartmentDto, Department>();
            CreateMap<UpdateDepartmentDto, Department>();

            CreateMap<Programme, ProgrammeDto>()
           .ForMember(
               dest => dest.DepartmentName,
               opt => opt.MapFrom(src => src.Department.Name));

            CreateMap<CreateProgrammeDto, Programme>();
            CreateMap<UpdateProgrammeDto, Programme>();

           
            CreateMap<AcademicTerm, AcademicTermDto>();
            CreateMap<CreateAcademicTermDto, AcademicTerm>();

            CreateMap<ChatMessage, ChatMessageDto>()
            .ForMember(
                dest => dest.SenderEmail,
                opt => opt.MapFrom(src => src.Sender.Email))
            .ForMember(
                dest => dest.SentAt,
                opt => opt.MapFrom(src => src.CreatedAt));

        }
    }
}
