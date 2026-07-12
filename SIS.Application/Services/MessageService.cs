using AutoMapper;
using SIS.Application.Common;
using SIS.Application.DTOs;
using SIS.Application.Interfaces;
using SIS.Domain.Models;
using StudentInformationSystem.Domain.Enums;
using StudentInformationSystem.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIS.Application.Services
{
    public class MessageService : IMessageService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IChatNotifier _chatNotifier;  

        public MessageService(IUnitOfWork uow, IMapper mapper, IChatNotifier chatNotifier)
        {
            _uow = uow;
            _mapper = mapper;
            _chatNotifier = chatNotifier;
        }

        public async Task<IList<ChatMessageDto>> GetConversationAsync(int userId1, int userId2)
        {
            var messages = await _uow.ChatMessages.GetConversationAsync(userId1, userId2);
            return _mapper.Map<IList<ChatMessageDto>>(messages);
        }

        public async Task<ChatMessageDto> SendMessageAsync(int senderId, SendMessageDto dto)
        {
            var sender = await _uow.Users.GetByIdAsync(senderId);
            if (sender == null)
                throw new NotFoundException(ErrorMessages.UserNotFound);

            var receiver = await _uow.Users.GetByIdAsync(dto.ReceiverId);
            if (receiver == null)
                throw new NotFoundException(ErrorMessages.UserNotFound);

            var allowed = await CanCommunicateAsync(sender, receiver);
            if (!allowed)
                throw new ForbiddenException(
                    "Yalnız öz kursunuzdakı müəllim/tələbələrlə yazışa bilərsiniz.");


            var message= new ChatMessage
           {
               SenderId = senderId,
               ReceiverId = dto.ReceiverId,
               Content = dto.Content
           };
            await _uow.ChatMessages.AddAsync(message);
            await _uow.SaveChangesAsync();

           
            var messageDto = new ChatMessageDto
            {
                Id = message.Id,
                SenderId = senderId,
                SenderEmail = sender?.Email ?? "",
                ReceiverId = dto.ReceiverId,
                Content = dto.Content,
                IsRead = false,
                SentAt = message.CreatedAt
            };

            await _chatNotifier.NotifyNewMessageAsync(dto.ReceiverId,messageDto);
            return messageDto;

        }

        private async Task<bool> CanCommunicateAsync(User sender, User receiver)
        {
            
            if (sender.Role == UserRole.Admin || receiver.Role == UserRole.Admin)
                return false;

           
            if (sender.Role == UserRole.Teacher && receiver.Role == UserRole.Student)
            {
                if (sender.TeacherId == null || receiver.StudentId == null)
                    return false;

              
                var teacherCourses = await _uow.Courses.GetByTeacherIdAsync(sender.TeacherId.Value);
                var studentEnrollments = await _uow.Enrollments.GetByStudentIdAsync(receiver.StudentId.Value);

                return teacherCourses.Any(course =>
                    studentEnrollments.Any(e => e.CourseId == course.Id));
            }

           
            if (sender.Role == UserRole.Student && receiver.Role == UserRole.Teacher)
            {
                if (sender.StudentId == null || receiver.TeacherId == null)
                    return false;

                var studentEnrollments = await _uow.Enrollments.GetByStudentIdAsync(sender.StudentId.Value);
                var teacherCourses = await _uow.Courses.GetByTeacherIdAsync(receiver.TeacherId.Value);

                return studentEnrollments.Any(e =>
                    teacherCourses.Any(course => course.Id == e.CourseId));
            }

           
            return false;
        }
    }
}
