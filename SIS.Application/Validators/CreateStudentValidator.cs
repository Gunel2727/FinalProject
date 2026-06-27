using FluentValidation;
using SIS.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIS.Application.Validators
{
    public class CreateStudentValidator:AbstractValidator<CreateStudentDto>
    {
        public CreateStudentValidator()
        {
           
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("Ad boş ola bilməz")
                .MaximumLength(50).WithMessage("Ad 50 simvoldan çox ola bilməz");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Soyad boş ola bilməz")
                .MaximumLength(50).WithMessage("Soyad 50 simvoldan çox ola bilməz");

           
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email boş ola bilməz")
                .EmailAddress().WithMessage("Email formatı yanlışdır");

            RuleFor(x => x.Phone)
                .NotEmpty().WithMessage("Telefon boş ola bilməz");

           
            RuleFor(x => x.AcademicYear)
                .InclusiveBetween(1, 4)
                .WithMessage("Kurs ili 1-4 arasında olmalıdır");

            
            RuleFor(x => x.ProgrammeId)
                .GreaterThan(0)
                .WithMessage("Proqram seçilməlidir");
        }
    }
}
