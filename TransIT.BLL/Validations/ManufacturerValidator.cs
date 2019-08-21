using FluentValidation;
using TransIT.BLL.DTOs;

namespace TransIT.BLL.Validations
{
    public class ManufacturerValidator : AbstractValidator<ManufacturerDTO>
    {
        public ManufacturerValidator()
        {
            RuleFor(m => m.Name)
                .NotEmpty()
                .NotNull();
        }
    }
}