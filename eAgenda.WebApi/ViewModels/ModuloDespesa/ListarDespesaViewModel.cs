using eAgenda.Dominio.ModuloDespesa;

namespace eAgenda.WebApi.ViewModels.ModuloDespesa
{
    public class ListarDespesaViewModel
    {
        public Guid Id { get; set; }
        public string Descricao { get; set; }
        public FormaPgtoDespesaEnum FormaPagamento { get; set; }
        public decimal Valor { get; set; }
        public string Data { get; set; }
    }
}
