using FluentValidation;
using GestionProductosAPI.DTOs;

namespace GestionProductosAPI.Validators
{
    public class ProductoInsertValidator : AbstractValidator<UpdateProductoDto>
    {
        public ProductoInsertValidator() 
        {
            RuleFor(x => x.Nombre).NotEmpty().WithMessage("El nombre es obligatorio")
                    .Length(1, 100).WithMessage("El nombre no debe exceder los 100 caracteres");

            RuleFor(x => x.Precio).GreaterThan(0).WithMessage("El precio debe ser mayor a 0");

            RuleFor(x => x.Stock).GreaterThanOrEqualTo(0).WithMessage("El stock no puede ser negativo");


        }
    }
}
