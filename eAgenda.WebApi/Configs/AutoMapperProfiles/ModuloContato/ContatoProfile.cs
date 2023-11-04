using eAgenda.Dominio.ModuloContato;
using eAgenda.WebApi.ViewModels.ModuloContato;

namespace eAgenda.WebApi.Configs.AutoMapperProfiles.ModuloContato
{
    public class ContatoProfile : Profile
    {
        public ContatoProfile()
        {
            CreateMap<Contato, ListarContatoViewModel>();
            CreateMap<Contato, VisualizarContatoViewModel>();
            CreateMap<Contato, FormContatoViewModel>();
            CreateMap<FormContatoViewModel, Contato>();
        }
    }
}
