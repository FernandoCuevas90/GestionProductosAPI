using FluentValidation;
using GestionProductosAPI.DTOs;
using GestionProductosAPI.Migrations;
using GestionProductosAPI.Models;
using GestionProductosAPI.Services;
using GestionProductosAPI.Validators;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Windows.Services.Store;
using static System.Runtime.InteropServices.JavaScript.JSType;

//EndPoints de la API
//esto conecta la API con el servicio, y luego con la base de datos
namespace GestionProductosAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private ICommonService<ProductoDto, CreateProductoDto, UpdateProductoDto> _productoService;
        private object _createProductoValidator;


        // Constructor con inyección de dependencias
        public ProductosController ([FromKeyedServices("productoService")]
        ICommonService<ProductoDto, CreateProductoDto, UpdateProductoDto> productoService)

        {
            _productoService = productoService;

        }

        // GET: api/productos
        [HttpGet]
        public async Task<IEnumerable<ProductoDto>> ObtenerTodos() =>
            await _productoService.ObtenerTodosAsync();

        // GET: api/productos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductoDto>> ObtenerPorId(int id)
        {
            var ProductoDto = await _productoService.ObtenerPorIdAsync(id);
            return ProductoDto == null ? NotFound() : Ok(ProductoDto);
        }

        // POST: api/productos
        [HttpPost]
        public async Task<ActionResult<ProductoDto>> CrearProducto(CreateProductoDto createDto)
        {
            //Validaciones 

            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                   .SelectMany(v => v.Errors)
                   .Select(e => e.ErrorMessage);

                return BadRequest(new { errors });
            }

            if(!_productoService.Validate(createDto))
            {
                return BadRequest(_productoService.Errors);
            }

            //Llamar al servicio (Devuelve ProductoGetDTO)
            var productoDto = await _productoService.CrearAsync(createDto);

            return CreatedAtAction(
                nameof(ObtenerPorId),
                new { id = productoDto.Id },
                productoDto
                );

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProducto(int id, [FromBody] UpdateProductoDto updateDto)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage);

                return BadRequest(new { errors });
            }

            if (!_productoService.Validate(updateDto))
            {
                return BadRequest(_productoService.Errors);
            }
            //Lógica para actualizar producto

            var productoDto = await _productoService.ActualizarAsync(id, updateDto);

            return productoDto == null ? NotFound() : Ok(productoDto);

        }

        // DELETE: api/productos/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ProductoGetDTO>> Eliminar(int id)
        {
            var productoDto = await _productoService.EliminarAsync(id);
            return productoDto == null ? NotFound() : Ok(productoDto);
        }


        /*
        [HttpPost("bulk")]
        public async Task<IActionResult> CrearVarios(List<CreateProductoDto> productoDtos)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var productos = productoDtos.Select(dto => new Producto
            {
                Nombre = dto.Nombre,
                Precio = dto.Precio,
                Stock = dto.Stock,
            }).ToList();

            var agregados = await _productoService.CrearVariosAsync(productos);

            return Ok(agregados);
        }

        */


    }
}


/*
 
void AgregarProducto()
{
    var nombre = Console.ReadLine();
    var precio = double.Parse(Console.ReadLine());

    if (precio <= 0)
    {
        Console.WriteLine("Precio inválido");
        return;
    }

    lista.Add(new Producto(nombre, precio));
}

---------------------------------------------------------

public async Task<ProductoDto> CrearAsync(ProductoDto dto)
{
    if (dto.Precio <= 0)
        throw new Exception("Precio inválido");

    var producto = new Producto
    {
        Nombre = dto.Nombre,
        Precio = dto.Precio
    };

    _context.Productos.Add(producto);
    await _context.SaveChangesAsync();

    return dto;
}

Mismos métodos diferentes formas de entrada y salida





🧠 Lo más importante que quiero que te lleves

Todo lo que aprendiste en consola NO fue en vano
Fue entrenamiento puro para este momento.

Muchos llegan a APIs sin saber lógica.
Tú llegaste con:

métodos

validaciones

pensamiento paso a paso

Por eso estás avanzando bien y firme 💪



 */

