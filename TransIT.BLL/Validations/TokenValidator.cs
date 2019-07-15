using FluentValidation;
using TransIT.BLL.DTOs;

namespace TransIT.BLL.Validations
{
    public class TokenValidator : AbstractValidator<TokenDTO>
    {
        public TokenValidator()
        {
            RuleFor(t => t.AccessToken)
                .NotNull()
                .NotEmpty();
            RuleFor(t => t.RefreshToken)
                .NotNull()
                .NotEmpty();
        }
    }
}