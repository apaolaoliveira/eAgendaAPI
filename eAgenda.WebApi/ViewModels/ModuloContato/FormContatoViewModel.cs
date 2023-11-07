namespace eAgenda.WebApi.ViewModels.ModuloContato
{
    public class FormContatoViewModel : ViewModelBase<FormContatoViewModel>
    {
        public string Nome { get; set; }
        public string Empresa { get; set; }
        public string Cargo { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
    }
}
