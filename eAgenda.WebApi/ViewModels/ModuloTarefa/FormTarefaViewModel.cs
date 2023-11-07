using eAgenda.Dominio.ModuloTarefa;

namespace eAgenda.WebApi.ViewModels.ModuloTarefa
{
    public class FormTarefaViewModel : ViewModelBase<FormTarefaViewModel>
    {
        public string Titulo { get; set; }
        public PrioridadeTarefaEnum Prioridade { get; set; }
        public List<FormItemTarefaViewModel> Itens { get; set; }
    }
}
