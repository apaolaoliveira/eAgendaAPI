using eAgenda.Dominio.ModuloCategoria;
using eAgenda.WebApi.ViewModels.ModuloCategoria;

namespace eAgenda.WebApi.Configs.AutoMapperProfiles.ModuloCategoria
{
    public class CategoriaProfile : Profile
    {
        public CategoriaProfile()
        {
            CreateMap<Categoria, FormCategoriaViewModel>();
            CreateMap<Categoria, ListarCategoriaViewModel>();
            CreateMap<Categoria, VisualizarCategoriaViewModel>();
            CreateMap<FormCategoriaViewModel, Categoria>();
        }
    }
}
