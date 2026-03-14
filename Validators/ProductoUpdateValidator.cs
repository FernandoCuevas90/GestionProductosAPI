using FluentValidation;
using GestionProductosAPI.DTOs;


namespace GestionProductosAPI.Validators
{
    public class ProductoUpdateValidator : AbstractValidator<UpdateProductoDto>
    {
        public ProductoUpdateValidator() 
        {
            RuleFor(x => x.Nombre).NotEmpty().WithMessage("El nombre es obligatorio");

            RuleFor(x => x.Nombre).Length(1, 100).WithMessage("El nombre debe tener entre 1 y 100 caracteres");

            RuleFor(x => x.Precio).GreaterThan(0).WithMessage("El precio debe ser mayor a 0");

            RuleFor(x => x.Stock).GreaterThanOrEqualTo(0).WithMessage("El stock no puede ser negativo");
        }
    }
}
