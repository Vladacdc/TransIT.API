using FluentValidation;
using TransIT.BLL.DTOs;

namespace TransIT.BLL.Validations
{
    public class VehicleTypeValidator : AbstractValidator<VehicleTypeDTO>
    {
        public VehicleTypeValidator()
        {
            RuleFor(t => t.Name)
                .NotNull()
                .NotEmpty();
        }
    }
}
