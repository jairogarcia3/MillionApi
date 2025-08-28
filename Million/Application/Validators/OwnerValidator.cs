using FluentValidation;
using Application.DTOs;

namespace Application.Validators
{
    public class OwnerValidator : AbstractValidator<OwnerDto>
    {
        public OwnerValidator()
        {
            RuleFor(o => o.Name)
                .NotEmpty().WithMessage("Se debe ingresar un nombre.")                
                .MaximumLength(100).WithMessage("El nombre no debe tener mas de 100 caracteres");

            RuleFor(o => o.Address)
                .NotEmpty().WithMessage("Se debe ingresar una direccion.")
                .MaximumLength(100).WithMessage("La direccion no puede tener mas de 100 caracteres");

            RuleFor(o => o.Birthday)
                .LessThan(DateTime.Now).WithMessage("La fecha de nacimiento debe ser menor a la actual")               
                .When(o => o.Birthday.HasValue);
        }
    }
}
