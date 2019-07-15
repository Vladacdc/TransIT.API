using FluentValidation;
using TransIT.BLL.DTOs;

namespace TransIT.BLL.Validations
{
    public class IssueLogValidator : AbstractValidator<IssueLogDTO>
    {
        public IssueLogValidator()
        {
            RuleFor(t => t.Issue)
                .NotNull();
            RuleFor(t => t.Issue.Id)
                .NotNull()
                .GreaterThan(0);
            RuleFor(t => t.ActionType)
                .NotNull();
            RuleFor(t => t.ActionType.Id)
                .NotNull()
                .GreaterThan(0);
            RuleFor(t => t.NewState)
                .NotNull();
            RuleFor(t => t.NewState.Id)
                .NotNull()
                .GreaterThan(0);
        }
    }
}
