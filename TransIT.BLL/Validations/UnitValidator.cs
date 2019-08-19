using FluentValidation;
using TransIT.BLL.DTOs;

namespace TransIT.BLL.Validations
{
    public class UnitValidator : AbstractValidator<UnitDTO>
    {
        public UnitValidator()
        {
            RuleFor(u => u.Name)
                .NotNull()
                .NotEmpty();
            RuleFor(u => u.ShortName)
                .NotNull()
                .NotEmpty();
        }
    }
}