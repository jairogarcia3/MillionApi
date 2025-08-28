using FluentValidation;
using Application.DTOs;


namespace Application.Validators
{
    public class PropertyTracesValidator : AbstractValidator<PropertyTraceDto>
    {
        public PropertyTracesValidator()
        {
            RuleFor(o => o.Name)
               .NotEmpty().WithMessage("Se debe ingresar un nombre.")
               .MaximumLength(100).WithMessage("El nombre no debe tener mas de 100 caracteres");

            RuleFor(o => o.Value)
                .GreaterThan(0).WithMessage("el precio debe ser mayor a 0");

            RuleFor(o => o.Tax)
                .GreaterThan(0).WithMessage("los impuestos deben ser mayor a 0");

            RuleFor(o => o.PropertyIdProperty)
                .GreaterThan(0).WithMessage("debe ingresar una propiedad valida");
        }
    }
}
