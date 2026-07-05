using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SIS.Application.DTOs.AuthDto;

namespace SIS.Application.Validators
{
    public class RegisterValidator: AbstractValidator<RegisterDto>
    {
        public RegisterValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email boş ola bilməz")
                .EmailAddress().WithMessage("Email formatı yanlışdır");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Şifrə boş ola bilməz")
                .MinimumLength(6).WithMessage("Şifrə minimum 6 simvol olmalıdır");

            RuleFor(x => x.Role)
                .NotEmpty().WithMessage("Rol seçilməlidir")
                .Must(r => r is "Admin" or "Teacher" or "Student")
                .WithMessage("Rol Admin, Teacher və ya Student olmalıdır");
        }
    }
}
