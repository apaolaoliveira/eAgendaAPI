using eAgenda.Dominio.ModuloTarefa;

namespace eAgenda.WebApi.ViewModels.ModuloTarefa
{
    public class FormItemTarefaViewModel
    {
        public Guid Id { get; set; }
        public string Titulo { get; set; }
        public StatusItemTarefaEnum Status { get; set; }
        public bool Concluido { get; set; }
    }
}
