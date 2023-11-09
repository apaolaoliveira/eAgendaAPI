using eAgenda.Dominio.ModuloTarefa;
using eAgenda.WebApi.ViewModels.ModuloTarefa;

namespace eAgenda.WebApi.Configs.AutoMapperProfiles.ModuloTarefas
{
    public class TarefaProfile : Profile
    {
        public TarefaProfile()
        {
            CreateMap<Tarefa, FormTarefaViewModel>();
            CreateMap<Tarefa, ListarTarefaViewModel>()
                .ForMember(des => des.DataCriacao, opt => opt.MapFrom(origem => origem.DataCriacao.ToShortDateString()));

            CreateMap<Tarefa, VisualizarTarefaViewModel>()
                .ForMember(des => des.DataCriacao, opt => opt.MapFrom(origem => origem.DataCriacao.ToShortDateString()));

            CreateMap<FormTarefaViewModel, Tarefa>();            
        }
    }
}
