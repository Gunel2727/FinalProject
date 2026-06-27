using FluentValidation;
using SIS.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIS.Application.Validators
{
    public class CreateGradeValidator:AbstractValidator<CreateGradeDto>
    {
        public CreateGradeValidator()
        {
            RuleFor(x => x.StudentId)
                .GreaterThan(0)
                .WithMessage("Tələbə seçilməlidir");

            RuleFor(x => x.CourseId)
                .GreaterThan(0)
                .WithMessage("Kurs seçilməlidir");

           
            RuleFor(x => x.Score)
                .InclusiveBetween(0, 100)
                .WithMessage("Bal 0-100 arasında olmalıdır");
        }
    }
}
