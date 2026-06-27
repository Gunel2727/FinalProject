using FluentValidation;
using SIS.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIS.Application.Validators
{
    public class CreateAnnouncementValidator:AbstractValidator<CreateAnnouncementDto>
    {
        public CreateAnnouncementValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Başlıq boş ola bilməz")
                .MaximumLength(200).WithMessage("Başlıq 200 simvoldan çox ola bilməz");

            RuleFor(x => x.Content)
                .NotEmpty().WithMessage("Məzmun boş ola bilməz");

           
            RuleFor(x => x.CourseId)
                .GreaterThan(0)
                .When(x => !x.IsGlobal)
                .WithMessage("Kurs spesifik elan üçün kurs seçilməlidir");
        }
    }
}
