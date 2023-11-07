namespace eAgenda.WebApi.ViewModels.ModuloContato
{
    public class ListarContatoViewModel : ViewModelBase<ListarContatoViewModel>
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Empresa { get; set; }
        public string Cargo { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public bool Favorito { get; set; }
    }
}
