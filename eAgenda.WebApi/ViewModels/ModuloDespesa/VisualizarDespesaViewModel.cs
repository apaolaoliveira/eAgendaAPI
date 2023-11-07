using eAgenda.Dominio.ModuloDespesa;
using eAgenda.WebApi.ViewModels.ModuloCategoria;

namespace eAgenda.WebApi.ViewModels.ModuloDespesa
{
    public class VisualizarDespesaViewModel : ViewModelBase<VisualizarDespesaViewModel>
    {
        public Guid Id { get; set; }
        public string Descricao { get; set; }
        public decimal Valor { get; set; }
        public FormaPgtoDespesaEnum FormaPagamento { get; set; }
        public string Data { get; set; }
        public List<ListarCategoriaViewModel> Categorias { get; set; }
    }
}
