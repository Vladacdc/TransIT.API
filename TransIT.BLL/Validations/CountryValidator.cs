using FluentValidation;
using TransIT.BLL.DTOs;

namespace TTransIT.BLL.Validations
{
    public class CountryValidator : AbstractValidator<CountryDTO>
    {
        public CountryValidator()
        {
            RuleFor(t => t.Name)
                .NotNull()
                .NotEmpty();
        }
    }
}
