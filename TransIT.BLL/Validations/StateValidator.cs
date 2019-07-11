using FluentValidation;
using TransIT.BLL.DTOs;

namespace TransIT.BLL.Validations
{
    public class StateValidator : AbstractValidator<StateDTO>
    {
        public StateValidator()
        {
            RuleFor(t => t.TransName)
                .NotNull()
                .NotEmpty();
        }
    }
}
