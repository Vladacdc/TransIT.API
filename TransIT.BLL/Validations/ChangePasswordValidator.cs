using FluentValidation;
using TransIT.BLL.DTOs;

namespace TransIT.BLL.Validations
{
    public class ChangePasswordValidator : AbstractValidator<ChangePasswordDTO>
    {
        public ChangePasswordValidator()
        {
            RuleFor(p => p.Password)
                .NotNull()
                .NotEmpty()
                .MinimumLength(6)
                .MaximumLength(30);
        }
    }
}
