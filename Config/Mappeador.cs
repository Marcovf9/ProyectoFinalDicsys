using AutoMapper;
using ProyectoFinalDicsys.Models;
using ProyectoFinalDicsys.DTOs;

namespace ProyectoFinalDicsys.Config
{
    public class Mappeador : Profile
    {
        public Mappeador()
        {
            CreateMap<Producto, ProductoDTO>();

            CreateMap<ProductoDTO, Producto>()
                .ForMember(dest => dest.Categoria, opt => opt.Ignore());

            CreateMap<Categoria, CategoriaDTO>();
            CreateMap<CategoriaDTO, Categoria>();
        }
    }
}
