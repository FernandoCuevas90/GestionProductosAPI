using GestionProductosAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using GestionProductosAPI.DTOs;

namespace GestionProductosAPI.Services
{
    public interface ICommonService<T, TI, TU> //esta es la interfaz (contrato) define lo que puede hacer el servicio
    {
        public List<string> Errors {  get; }
            Task<IEnumerable<T>> ObtenerTodosAsync();
            Task<T> ObtenerPorIdAsync(int id);
            Task<T> CrearAsync(TI CreateProductoDto);
            Task<T> ActualizarAsync(int id, TU UpdateProductoDto);
            Task<T> EliminarAsync(int id);

        bool Validate(TI dto);
        bool Validate(TU dto);

        
        //Task CrearAsync(ProductoDto productoDto);

        //Agregar varios productos
        //Task<List<Producto>> CrearVariosAsync(List<Producto> productos);
    }

    }

