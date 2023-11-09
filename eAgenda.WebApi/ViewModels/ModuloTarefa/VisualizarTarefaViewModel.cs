using eAgenda.Dominio.ModuloTarefa;

namespace eAgenda.WebApi.ViewModels.ModuloTarefa
{
    public class VisualizarTarefaViewModel
    {
        public Guid Id { get; set; }
        public string Titulo { get; set; }
        public string DataCriacao { get; set; }
        public string? DataConclusao { get; set; }

        public List<VisualizarItemTarefaViewModel> Itens { get; set; }
        public int quantidadeItens { get { return Itens.Count; } }
        public decimal PercentualConcluido { get; set; }

        public PrioridadeTarefaEnum Prioridade { get; set; }
        public StatusItemTarefaEnum Status { get; set; }

        public VisualizarTarefaViewModel()
        {
            Itens = new List<VisualizarItemTarefaViewModel>();
        }
    }
}
