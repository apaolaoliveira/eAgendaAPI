using eAgenda.Dominio.ModuloTarefa;
using eAgenda.WebApi.ViewModels.ModuloTarefa;

namespace eAgenda.WebApi.Configs.AutoMapperProfiles.ModuloTarefas
{
    public class TarefaProfile : Profile
    {
        public TarefaProfile()
        {
            CreateMap<Tarefa, FormTarefaViewModel>();
            CreateMap<Tarefa, ListarTarefaViewModel>();
            CreateMap<Tarefa, VisualizarTarefaViewModel>();
            CreateMap<FormTarefaViewModel, Tarefa>();
        }
    }
}
