using eAgenda.Aplicacao.ModuloTarefa;
using eAgenda.Dominio.ModuloTarefa;
using eAgenda.WebApi.ViewModels.ModuloTarefa;

namespace eAgenda.WebApi.Controllers.ModuloTarefa
{
    [ApiController]
    [Route("api/tarefas")]
    public class TarefaController : ApiControllerBase
    {
        private readonly ServicoTarefa _servicoTarefa;
        private readonly IMapper _mapeador;

        public TarefaController(ServicoTarefa servicoTarefa, IMapper mapeador)
        {
            _servicoTarefa = servicoTarefa;
            _mapeador = mapeador;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ListarTarefaViewModel), 200)]
        [ProducesResponseType(typeof(string[]), 500)]
        public IActionResult SelecionarTodos(StatusTarefaEnum statusSelecionado)
        {
            Result<List<Tarefa>> resultadoBusca = _servicoTarefa.SelecionarTodos(statusSelecionado);

            if (resultadoBusca.IsFailed) return StatusNotFound(resultadoBusca);

            return ProcessarResultado(resultadoBusca, _mapeador.Map<List<ListarTarefaViewModel>>(resultadoBusca.Value));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(FormTarefaViewModel), 200)]
        [ProducesResponseType(typeof(string[]), 404)]
        [ProducesResponseType(typeof(string[]), 500)]
        public IActionResult SelecionarPorId(Guid id)
        {
            Result<Tarefa> resultadoBusca = _servicoTarefa.SelecionarPorId(id);

            if (resultadoBusca.IsFailed) return StatusNotFound(resultadoBusca);

            return ProcessarResultado(resultadoBusca, _mapeador.Map<FormTarefaViewModel>(resultadoBusca.Value));
        }

        [HttpGet("visualizacao-completa/{id}")]
        [ProducesResponseType(typeof(VisualizarTarefaViewModel), 200)]
        [ProducesResponseType(typeof(string[]), 404)]
        [ProducesResponseType(typeof(string[]), 500)]
        public IActionResult SelecionarCompletoPorId(Guid id)
        {
            Result<Tarefa> resultadoBusca = _servicoTarefa.SelecionarPorId(id);

            if (resultadoBusca.IsFailed) return StatusNotFound(resultadoBusca);

            return ProcessarResultado(resultadoBusca, _mapeador.Map<VisualizarTarefaViewModel>(resultadoBusca.Value));
        }

        [HttpPost]
        [ProducesResponseType(typeof(FormTarefaViewModel), 200)]
        [ProducesResponseType(typeof(string[]), 400)]
        [ProducesResponseType(typeof(string[]), 500)]
        public IActionResult Inserir(FormTarefaViewModel tarefaViewModel)
        {
            Result<Tarefa> resultado = _servicoTarefa.Inserir(_mapeador.Map<Tarefa>(tarefaViewModel));

            return ProcessarResultado(resultado, tarefaViewModel);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(FormTarefaViewModel), 200)]
        [ProducesResponseType(typeof(string[]), 400)]
        [ProducesResponseType(typeof(string[]), 404)]
        [ProducesResponseType(typeof(string[]), 500)]
        public IActionResult Editar(Guid id, FormTarefaViewModel tarefaViewModel)
        {
            Result<Tarefa> resultadoBusca = _servicoTarefa.SelecionarPorId(id);

            if (resultadoBusca.IsFailed) return StatusNotFound(resultadoBusca);

            Result<Tarefa> resultado = _servicoTarefa.Editar(_mapeador.Map(tarefaViewModel, resultadoBusca.Value));

            return ProcessarResultado(resultado, tarefaViewModel);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(FormTarefaViewModel), 200)]
        [ProducesResponseType(typeof(string[]), 400)]
        [ProducesResponseType(typeof(string[]), 404)]
        [ProducesResponseType(typeof(string[]), 500)]
        public IActionResult Excluir(Guid id)
        {
            Result<Tarefa> resultadoBusca = _servicoTarefa.SelecionarPorId(id);

            if (resultadoBusca.IsFailed) return StatusNotFound(resultadoBusca);

            return ProcessarResultado<Tarefa>(_servicoTarefa.Excluir(resultadoBusca.Value));
        }
    }
}
