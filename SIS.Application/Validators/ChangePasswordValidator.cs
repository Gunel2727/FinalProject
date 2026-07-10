using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SIS.Application.DTOs.AuthDto;

namespace SIS.Application.Validators
{
    public class ChangePasswordValidator:AbstractValidator<ChangePasswordDto>
    {
        public ChangePasswordValidator()
        {
            RuleFor(x => x.OldPassword)
                .NotEmpty().WithMessage("Köhnə şifrə boş ola bilməz");

            RuleFor(x => x.NewPassword)
                .NotEmpty().WithMessage("Yeni şifrə boş ola bilməz")
                .MinimumLength(6).WithMessage("Yeni şifrə minimum 6 simvol olmalıdır")
                .NotEqual(x => x.OldPassword).WithMessage("Yeni şifrə köhnə şifrə ilə eyni ola bilməz");
        }
    }
}
