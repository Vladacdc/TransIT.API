using FluentValidation;
using TransIT.BLL.DTOs;

namespace TransIT.BLL.Validations
{
    public class CurrencyValidator : AbstractValidator<CurrencyDTO>
    {
        public CurrencyValidator()
        {
            RuleFor(t => t.FullName)
                .NotNull()
                .NotEmpty();
            RuleFor(t => t.ShortName)
                .NotNull()
                .NotEmpty();
        }
    }
}
