using FluentValidation;
using TransIT.BLL.DTOs;

namespace TransIT.BLL.Validations
{
    public class DocumentValidator : AbstractValidator<DocumentDTO>
    {
        public DocumentValidator()
        {
            RuleFor(t => t.Name)
                .NotNull()
                .NotEmpty();
            RuleFor(t => t.Description)
                .NotNull()
                .NotEmpty();
            RuleFor(t => t.Path)
                .NotNull()
                .NotEmpty();
        }
    }
}
