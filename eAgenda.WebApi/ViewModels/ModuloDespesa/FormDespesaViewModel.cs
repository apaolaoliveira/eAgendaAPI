using eAgenda.Dominio.ModuloDespesa;

namespace eAgenda.WebApi.ViewModels.ModuloDespesa
{
    public class FormDespesaViewModel
    {
        public string Descricao { get; set; }
        public decimal Valor { get; set; }
        public string Data { get; set; }
        public FormaPgtoDespesaEnum FormaPagamento { get; set; }
        public Guid[] categoriasSelecionadas { get; set; }
    }
}
