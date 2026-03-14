using AutoMapper;
using GestionProductosAPI.Data;
using GestionProductosAPI.DTOs;
using GestionProductosAPI.IRepository;
using GestionProductosAPI.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace GestionProductosAPI.Services
{
    public class ProductoService : ICommonService<ProductoDto, CreateProductoDto, UpdateProductoDto>
    {


        private IRepository<Producto> _productoRepository;
        private IMapper _mapper;

        public List<string> Errors { get; }

        public ProductoService(IRepository<Producto> productoRepository,
            IMapper mapper)
        {
            _productoRepository = productoRepository;
            _mapper = mapper;
            Errors = new List<string>();
        }

        public async Task<IEnumerable<ProductoDto>> ObtenerTodosAsync()
        {
            var productos = await _productoRepository.Get();

            return productos.Select(p => _mapper.Map<ProductoDto>(p));
        }

        public async Task<ProductoDto> ObtenerPorIdAsync(int id)
        {
            var producto = await _productoRepository.GetByID(id);

            if (producto != null)
            {
                var ProductoDto = _mapper.Map<ProductoDto>(producto);

                return ProductoDto;
            }

            return null;
        }

        public async Task<ProductoDto> CrearAsync(CreateProductoDto createDto)
        {
            var producto = _mapper.Map<Producto>(createDto);

            await _productoRepository.Add(producto);
            await _productoRepository.save();

            var productoDto = _mapper.Map<ProductoDto>(producto);

            return productoDto;

        }

        public async Task<ProductoDto> ActualizarAsync(int id, UpdateProductoDto updateDto)
        {
            var producto = await _productoRepository.GetByID(id);

            if (producto != null)

            {
                producto = _mapper.Map<UpdateProductoDto, Producto>(updateDto, producto);

                _productoRepository.Update(producto);
                await _productoRepository.save();

                var productoDto = _mapper.Map<ProductoDto>(producto);

                return productoDto;
            }
            return null;
        }

        public async Task<ProductoDto> EliminarAsync(int id)
        {
            var producto = await _productoRepository.GetByID(id);

            if (producto != null)
            {

                var productoDto = _mapper.Map<ProductoDto>(producto);

                _productoRepository.Delete(producto);
                await _productoRepository.save(); //Cambios en la base de datos

                return productoDto;
            }

            return null;
        }

        public bool Validate(CreateProductoDto createProductoDto)
        {
            if(_productoRepository.Search(p => p.Nombre == createProductoDto.Nombre).Count() > 0)
            {
                Errors.Add("No puede existir un producto con un nombre ya existente");
                return false;
            }
            return true;
        }

        public bool Validate(UpdateProductoDto updateProductoDto)
        {
            if (_productoRepository.Search(p => p.Nombre == updateProductoDto.Nombre 
            && updateProductoDto.Id != p.Id).Count() > 0)
            {
                Errors.Add("No puede existir un producto con un nombre ya existente");
                return false;
            }
            return true;
        }

        //este método crea masivamente los productos
        /*public async Task<List<Producto>> CrearVariosAsync(List<Producto> productos)
        {
            _context.Productos.AddRange(productos);
            await _context.SaveChangesAsync();
            return productos;
        }

        */
    }
}


/*
Pantalla (Frontend / UI)
        ↓
Controller (API)
        ↓
IProductoService (contrato)
        ↓
ProductoService (implementación)
        ↓
Base de datos




 */