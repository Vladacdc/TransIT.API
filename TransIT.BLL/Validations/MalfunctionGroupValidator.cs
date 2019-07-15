using FluentValidation;
using TransIT.BLL.DTOs;

namespace TransIT.BLL.Validations
{
    public class MalfunctionGroupValidator : AbstractValidator<MalfunctionGroupDTO>
    {
        public MalfunctionGroupValidator()
        {
            RuleFor(t => t.Name)
                .NotNull()
                .NotEmpty();
        }
    }
}
