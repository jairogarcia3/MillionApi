using FluentValidation;
using Application.DTOs;

namespace Application.Validators
{
    public class PropertyImageValidator : AbstractValidator<PropertyImageDto>
    {
        public PropertyImageValidator()
        {
            RuleFor(o => o.File)
                .NotEmpty().WithMessage("Se debe ingresar un archivo.");               

            RuleFor(o => o.PropertyIdProperty)
                .GreaterThan(0).WithMessage("debe ingresar una propiedad valida");
        }
    }
}
