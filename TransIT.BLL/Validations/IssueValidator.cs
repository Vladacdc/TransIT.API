using FluentValidation;
using TransIT.BLL.DTOs;

namespace TransIT.BLL.Validations
{
    public class IssueValidator : AbstractValidator<IssueDTO>
    {
        public IssueValidator()
        {
            RuleFor(t => t.Summary)
                .NotNull()
                .NotEmpty();
            RuleFor(x => x.Vehicle)
                .NotNull();
            RuleFor(x => x.Vehicle.Id)
                .NotNull()
                .GreaterThan(0);
        }
    }
}
