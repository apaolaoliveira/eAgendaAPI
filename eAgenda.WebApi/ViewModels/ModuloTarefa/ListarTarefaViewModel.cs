using eAgenda.Dominio.ModuloTarefa;

namespace eAgenda.WebApi.ViewModels.ModuloTarefa
{
    public class ListarTarefaViewModel
    {
        public Guid Id { get; set; }
        public string Titulo { get; set; }
        public string DataCriacao { get; set; }
        public PrioridadeTarefaEnum Prioridade { get; set; }
        public string Status { get; set; }
    }
}
