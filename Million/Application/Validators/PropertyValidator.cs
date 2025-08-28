using FluentValidation;
using Application.DTOs;

namespace Application.Validators
{
    public class PropertyValidator : AbstractValidator<PropertyDto>
    {
        public PropertyValidator()
        {
            RuleFor(o => o.Name)
                .NotEmpty().WithMessage("Se debe ingresar un nombre.")
                .MaximumLength(100).WithMessage("El nombre no debe tener mas de 100 caracteres");

            RuleFor(o => o.Address)
                .NotEmpty().WithMessage("Se debe ingresar una direccion.")
                .MaximumLength(100).WithMessage("La direccion no puede tener mas de 100 caracteres");

            RuleFor(o => o.Price)
                .GreaterThan(0).WithMessage("el precio debe ser mayor a 0");

            RuleFor(o => o.Year)
              .GreaterThan(0).WithMessage("el valor debe ser mayor a 0");

            RuleFor(o => o.CodeInternal)
              .NotEmpty().WithMessage("Se debe ingresar un codigo.");

            RuleFor(o => o.OwnerIdOwner)
              .NotEmpty().WithMessage("Se debe ingresar un codigo propietario.");
        }
    }
}
