using FluentValidation;
using SIS.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIS.Application.Validators
{
    public class CreateCourseValidator:AbstractValidator<CreateCourseDto>
    {
        public CreateCourseValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Kurs adı boş ola bilməz")
                .MaximumLength(100).WithMessage("Kurs adı 100 simvoldan çox ola bilməz");

            RuleFor(x => x.Code)
                .NotEmpty().WithMessage("Kurs kodu boş ola bilməz")
                .MaximumLength(10).WithMessage("Kurs kodu 10 simvoldan çox ola bilməz");

            
            RuleFor(x => x.Credits)
                .InclusiveBetween(1, 6)
                .WithMessage("Kredit sayı 1-6 arasında olmalıdır");

            RuleFor(x => x.TeacherId)
                .GreaterThan(0)
                .WithMessage("Müəllim seçilməlidir");

            RuleFor(x => x.AcademicTermId)
                .GreaterThan(0)
                .WithMessage("Semestr seçilməlidir");
        }
    }
}
