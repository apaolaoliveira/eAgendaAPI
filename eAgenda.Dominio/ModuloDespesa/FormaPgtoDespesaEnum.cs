using System.ComponentModel;

namespace eAgenda.Dominio.ModuloDespesa
{
    public enum FormaPgtoDespesaEnum
    {

        [Description("Pix")]
        PIX,

        [Description("Dinheiro")]
        Dinheiro,

        [Description("Cartão")]
        CartaoCredito
    }

}
