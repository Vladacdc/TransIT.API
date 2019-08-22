using FluentValidation;
using TransIT.BLL.DTOs;

namespace TransIT.BLL.Validations
{
    public class PartValidator : AbstractValidator<PartDTO>
    {
        public PartValidator()
        {
            RuleFor(p => p.Unit)
                .NotNull()
                .NotEmpty();
            RuleFor(p => p.Manufacturer)
                .NotNull()
                .NotEmpty();
        }
    }
}