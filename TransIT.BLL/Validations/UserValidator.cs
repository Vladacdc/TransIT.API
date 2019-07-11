using FluentValidation;
using TransIT.BLL.DTOs;

namespace TransIT.BLL.Validations
{
    public class UserValidator : AbstractValidator<UserDTO>
    {
        public UserValidator()
        {
            RuleFor(u => u.Login)
                .Length(6, 50)
                .NotNull()
                .NotEmpty()
                .WithMessage("Login can not be empty");
            RuleFor(u => u.Email)
                .Null()
                .Empty()
                .EmailAddress();
            RuleFor(u => u.Role)
                .NotNull()
                .NotEmpty();
            RuleFor(u => u.Password)
                .NotNull()
                .NotEmpty()
                .Length(6, 25);
        }
    }
}