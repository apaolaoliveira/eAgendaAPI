using eAgenda.Dominio.ModuloTarefa;

namespace eAgenda.WebApi.ViewModels.ModuloTarefa
{
    public class ListarTarefaViewModel : ViewModelBase<ListarTarefaViewModel>
    {
        public Guid Id { get; set; }
        public string Titulo { get; set; }
        public DateTime DataCriacao { get; set; }
        public PrioridadeTarefaEnum Prioridade { get; set; }
        public string Status { get; set; }
    }
}
