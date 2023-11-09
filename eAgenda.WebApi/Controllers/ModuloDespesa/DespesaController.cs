using eAgenda.Aplicacao.ModuloDespesa;
using eAgenda.Dominio.ModuloDespesa;
using eAgenda.WebApi.ViewModels.ModuloDespesa;

namespace eAgenda.WebApi.Controllers.ModuloDespesa
{
    [ApiController]
    [Route("api/despesas")]
    public class DespesaController : ApiControllerBase
    {
        private readonly ServicoDespesa _servicoDespesa;
        private readonly IMapper _mapeador;

        public DespesaController(ServicoDespesa servicoDespesa, IMapper mapeador)
        {
            _servicoDespesa = servicoDespesa;
            _mapeador = mapeador;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ListarDespesaViewModel), 200)]
        [ProducesResponseType(typeof(string[]), 500)]
        public IActionResult SelecionarTodos()
        {
            Result<List<Despesa>> resultadoBusca = _servicoDespesa.SelecionarTodos();

            if (resultadoBusca.IsFailed) return StatusNotFound(resultadoBusca);

            return ProcessarResultado(resultadoBusca, _mapeador.Map<List<ListarDespesaViewModel>>(resultadoBusca.Value));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(FormDespesaViewModel), 200)]
        [ProducesResponseType(typeof(string[]), 404)]
        [ProducesResponseType(typeof(string[]), 500)]
        public IActionResult SelecionarPorId(Guid id)
        {
            Result<Despesa> resultadoBusca = _servicoDespesa.SelecionarPorId(id);

            if (resultadoBusca.IsFailed) return StatusNotFound(resultadoBusca);

            return ProcessarResultado(resultadoBusca, _mapeador.Map<FormDespesaViewModel>(resultadoBusca.Value));
        }

        [HttpGet("visualizacao-completa/{id}")]
        [ProducesResponseType(typeof(VisualizarDespesaViewModel), 200)]
        [ProducesResponseType(typeof(string[]), 404)]
        [ProducesResponseType(typeof(string[]), 500)]
        public IActionResult SelecionarCompletoPorId(Guid id)
        {
            Result<Despesa> resultadoBusca = _servicoDespesa.SelecionarPorId(id);

            if (resultadoBusca.IsFailed) return StatusNotFound(resultadoBusca);

            return ProcessarResultado(resultadoBusca, _mapeador.Map<VisualizarDespesaViewModel>(resultadoBusca.Value));
        }

        [HttpPost]
        [ProducesResponseType(typeof(FormDespesaViewModel), 200)]
        [ProducesResponseType(typeof(string[]), 400)]
        [ProducesResponseType(typeof(string[]), 500)]
        public IActionResult Inserir(FormDespesaViewModel despesaViewModel)
        {
            Result<Despesa> resultado = _servicoDespesa.Inserir(_mapeador.Map<Despesa>(despesaViewModel));

            return ProcessarResultado(resultado, despesaViewModel);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(FormDespesaViewModel), 200)]
        [ProducesResponseType(typeof(string[]), 400)]
        [ProducesResponseType(typeof(string[]), 404)]
        [ProducesResponseType(typeof(string[]), 500)]
        public IActionResult Editar(Guid id, FormDespesaViewModel despesaViewModel)
        {
            Result<Despesa> resultadoBusca = _servicoDespesa.SelecionarPorId(id);

            if (resultadoBusca.IsFailed) return StatusNotFound(resultadoBusca);

            Result<Despesa> resultado = _servicoDespesa.Editar(_mapeador.Map(despesaViewModel, resultadoBusca.Value));

            return ProcessarResultado(resultado, despesaViewModel);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(FormDespesaViewModel), 200)]
        [ProducesResponseType(typeof(string[]), 400)]
        [ProducesResponseType(typeof(string[]), 404)]
        [ProducesResponseType(typeof(string[]), 500)]
        public IActionResult Excluir(Guid id)
        {
            Result<Despesa> resultadoBusca = _servicoDespesa.SelecionarPorId(id);

            if (resultadoBusca.IsFailed) return StatusNotFound(resultadoBusca);

            return ProcessarResultado<Despesa>(_servicoDespesa.Excluir(resultadoBusca.Value));
        }
    }
}
