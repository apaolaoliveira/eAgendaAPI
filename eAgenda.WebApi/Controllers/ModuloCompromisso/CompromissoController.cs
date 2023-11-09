using eAgenda.Aplicacao.ModuloCompromisso;
using eAgenda.Dominio.ModuloCompromisso;
using eAgenda.WebApi.ViewModels.ModuloCompromisso;

namespace eAgenda.WebApi.Controllers.ModuloCompromisso
{
    [ApiController]
    [Route("api/compromissos")]
    public class CompromissoController : ApiControllerBase
    {
        private readonly ServicoCompromisso _servicoCompromisso;
        private readonly IMapper _mapeador;

        public CompromissoController(ServicoCompromisso servicoCompromisso, IMapper mapeador)
        {
            this._servicoCompromisso = servicoCompromisso;
            this._mapeador = mapeador;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ListarCompromissoViewModel), 200)]
        [ProducesResponseType(typeof(string[]), 500)]
        public IActionResult SelecionarTodos()
        {
            Result<List<Compromisso>> resultadoBusca = _servicoCompromisso.SelecionarTodos();

            if (resultadoBusca.IsFailed) return StatusNotFound(resultadoBusca);

            return ProcessarResultado(resultadoBusca, _mapeador.Map<List<ListarCompromissoViewModel>>(resultadoBusca.Value));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(FormCompromissoViewModel), 200)]
        [ProducesResponseType(typeof(string[]), 404)]
        [ProducesResponseType(typeof(string[]), 500)]
        public IActionResult SelecionarPorId(Guid id)
        {
            Result<Compromisso> resultadoBusca = _servicoCompromisso.SelecionarPorId(id);

            if (resultadoBusca.IsFailed) return StatusNotFound(resultadoBusca);

            return ProcessarResultado(resultadoBusca, _mapeador.Map<FormCompromissoViewModel>(resultadoBusca.Value));
        }

        [HttpGet("visualizacao-completa/{id}")]
        [ProducesResponseType(typeof(VisualizarCompromissoViewModel), 200)]
        [ProducesResponseType(typeof(string[]), 404)]
        [ProducesResponseType(typeof(string[]), 500)]
        public IActionResult SelecionarCompletoPorId(Guid id)
        {
            Result<Compromisso> resultadoBusca = _servicoCompromisso.SelecionarPorId(id);

            if (resultadoBusca.IsFailed) return StatusNotFound(resultadoBusca);

            return ProcessarResultado(resultadoBusca, _mapeador.Map<VisualizarCompromissoViewModel>(resultadoBusca.Value));
        }

        [HttpPost]
        [ProducesResponseType(typeof(FormCompromissoViewModel), 200)]
        [ProducesResponseType(typeof(string[]), 400)]
        [ProducesResponseType(typeof(string[]), 500)]
        public IActionResult Inserir(FormCompromissoViewModel compromissoViewModel)
        {
            Result<Compromisso> resultado = _servicoCompromisso.Inserir(_mapeador.Map<Compromisso>(compromissoViewModel));

            return ProcessarResultado(resultado, compromissoViewModel);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(FormCompromissoViewModel), 200)]
        [ProducesResponseType(typeof(string[]), 400)]
        [ProducesResponseType(typeof(string[]), 404)]
        [ProducesResponseType(typeof(string[]), 500)]
        public IActionResult Editar(Guid id, FormCompromissoViewModel compromissoViewModel)
        {
            Result<Compromisso> resultadoBusca = _servicoCompromisso.SelecionarPorId(id);

            if (resultadoBusca.IsFailed) return StatusNotFound(resultadoBusca);

            Result<Compromisso> resultado = _servicoCompromisso.Editar(_mapeador.Map(compromissoViewModel, resultadoBusca.Value));

            return ProcessarResultado(resultado, compromissoViewModel);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(FormCompromissoViewModel), 200)]
        [ProducesResponseType(typeof(string[]), 400)]
        [ProducesResponseType(typeof(string[]), 404)]
        [ProducesResponseType(typeof(string[]), 500)]
        public IActionResult Excluir(Guid id)
        {
            Result<Compromisso> resultadoBusca = _servicoCompromisso.SelecionarPorId(id);

            if (resultadoBusca.IsFailed) return StatusNotFound(resultadoBusca);

            return ProcessarResultado<Compromisso>(_servicoCompromisso.Excluir(resultadoBusca.Value));
        }
    }
}
