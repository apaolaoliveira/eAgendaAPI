using eAgenda.Dominio.ModuloCompromisso;
using eAgenda.WebApi.ViewModels.ModuloContato;

namespace eAgenda.WebApi.ViewModels.ModuloCompromisso
{
    public class VisualizarCompromissoViewModel
    {
        public Guid Id { get; set; }
        public string Assunto { get; set; }
        public TipoLocalizacaoCompromissoEnum TipoLocal { get; set; }
        public string Local { get; set; }
        public string Link { get; set; }

        public string Data { get; set; }
        public string HoraInicio { get; set; }
        public string HoraTermino { get; set; }

        public ListarContatoViewModel Contato { get; set; }
    }
}
