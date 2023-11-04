using eAgenda.WebApi.ViewModels.ModuloDespesa;

namespace eAgenda.WebApi.ViewModels.ModuloCategoria
{
    public class VisualizarCategoriaViewModel
    {
        public Guid Id { get; set; }
        public string Titulo { get; set; }
        public List<ListarDespesaViewModel> Despesas { get; set; }
    }
}
