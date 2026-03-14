using AutoMapper;
using GestionProductosAPI.DTOs;
using GestionProductosAPI.Models;

namespace GestionProductosAPI.Automappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //POST
            CreateMap<CreateProductoDto, Producto>();

            //GET //UPDATE
            CreateMap<ProductoDto, Producto>();
            CreateMap<Producto,  ProductoDto>()
                .ForMember(dto => dto.Id,
                m => m.MapFrom(p => p.Id));//Funciones de primera clase las cuales reciben un argumento
            CreateMap<UpdateProductoDto, Producto>();  
        }
    }
}
