using eAgenda.Aplicacao.ModuloContato;
using eAgenda.Dominio.ModuloContato;
using eAgenda.WebApi.ViewModels.ModuloContato;

namespace eAgenda.WebApi.Controllers.ModuloContato
{
    [ApiController]
    [Route("api/contatos")]
    public class ContatoController : ApiControllerBase
    {
        private readonly ServicoContato _servicoContato;
        private readonly IMapper _mapeador;

        public ContatoController(ServicoContato servicoContato, IMapper mapeador)
        {
            _servicoContato = servicoContato;
            _mapeador = mapeador;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ListarContatoViewModel), 200)]
        [ProducesResponseType(typeof(string[]), 500)]
        public IActionResult SelecionarTodos(StatusFavoritoEnum statusFavorito)
        {
            Result<List<Contato>> resultadoBusca = _servicoContato.SelecionarTodos(statusFavorito);

            if (resultadoBusca.IsFailed) return StatusNotFound(resultadoBusca);

            return ProcessarResultado(resultadoBusca, _mapeador.Map<List<ListarContatoViewModel>>(resultadoBusca.Value));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(FormContatoViewModel), 200)]
        [ProducesResponseType(typeof(string[]), 404)]
        [ProducesResponseType(typeof(string[]), 500)]
        public IActionResult SelecionarPorId(Guid id)
        {
            Result<Contato> resultadoBusca = _servicoContato.SelecionarPorId(id);

            if (resultadoBusca.IsFailed) return StatusNotFound(resultadoBusca);

            return ProcessarResultado(resultadoBusca, _mapeador.Map<FormContatoViewModel>(resultadoBusca.Value));
        }

        [HttpGet("visualizacao-completa/{id}")]
        [ProducesResponseType(typeof(VisualizarContatoViewModel), 200)]
        [ProducesResponseType(typeof(string[]), 404)]
        [ProducesResponseType(typeof(string[]), 500)]
        public IActionResult SelecionarCompletoPorId(Guid id)
        {
            Result<Contato> resultadoBusca = _servicoContato.SelecionarPorId(id);

            if (resultadoBusca.IsFailed) return StatusNotFound(resultadoBusca);

            return ProcessarResultado(resultadoBusca, _mapeador.Map<VisualizarContatoViewModel>(resultadoBusca.Value));
        }

        [HttpPost]
        [ProducesResponseType(typeof(FormContatoViewModel), 200)]
        [ProducesResponseType(typeof(string[]), 400)]
        [ProducesResponseType(typeof(string[]), 500)]
        public IActionResult Inserir(FormContatoViewModel contatoViewModel)
        {
            Result<Contato> resultado = _servicoContato.Inserir(_mapeador.Map<Contato>(contatoViewModel));

            return ProcessarResultado(resultado, contatoViewModel);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(FormContatoViewModel), 200)]
        [ProducesResponseType(typeof(string[]), 400)]
        [ProducesResponseType(typeof(string[]), 404)]
        [ProducesResponseType(typeof(string[]), 500)]
        public IActionResult Editar(Guid id, FormContatoViewModel contatoViewModel)
        {
            Result<Contato> resultadoBusca = _servicoContato.SelecionarPorId(id);

            if (resultadoBusca.IsFailed) return StatusNotFound(resultadoBusca);

            Result<Contato> resultado = _servicoContato.Editar(_mapeador.Map(contatoViewModel, resultadoBusca.Value));

            return ProcessarResultado(resultado, contatoViewModel);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(FormContatoViewModel), 200)]
        [ProducesResponseType(typeof(string[]), 400)]
        [ProducesResponseType(typeof(string[]), 404)]
        [ProducesResponseType(typeof(string[]), 500)]
        public IActionResult Excluir(Guid id)
        {
            Result<Contato> resultadoBusca = _servicoContato.SelecionarPorId(id);

            if (resultadoBusca.IsFailed) return StatusNotFound(resultadoBusca);

            return ProcessarResultado<Contato>(_servicoContato.Excluir(resultadoBusca.Value));
        }
    }
}
