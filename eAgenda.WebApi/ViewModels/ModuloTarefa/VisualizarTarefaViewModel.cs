using eAgenda.Dominio.ModuloTarefa;

namespace eAgenda.WebApi.ViewModels.ModuloTarefa
{
    public class VisualizarTarefaViewModel
    {
        public Guid Id { get; set; }
        public string Titulo { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime? DataConclusao { get; set; }

        public List<VisualizarItemTarefaViewModel> Itens { get; set; }
        public int quantidadeItens { get { return Itens.Count; } }
        public decimal PercentualConcluido { get; set; }

        public PrioridadeTarefaEnum Prioridade { get; set; }
        public StatusItemTarefaEnum Status { get; set; }
    }
}
