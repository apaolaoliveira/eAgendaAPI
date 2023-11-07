using eAgenda.Dominio.ModuloCompromisso;

namespace eAgenda.WebApi.ViewModels.ModuloCompromisso
{
    public class FormCompromissoViewModel : ViewModelBase<FormCompromissoViewModel>
    {
        public string Assunto { get; set; }
        public TipoLocalizacaoCompromissoEnum TipoLocal { get; set; }
        public string Local { get; set; }
        public string Link { get; set; }

        public DateTime Data { get; set; }
        public string HoraInicio { get; set; }
        public string HoraTermino { get; set; }

        public Guid ContatoId { get; set; }
    }
}
