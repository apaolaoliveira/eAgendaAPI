using eAgenda.Aplicacao.ModuloCategoria;
using eAgenda.Dominio.ModuloCategoria;
using eAgenda.WebApi.ViewModels.ModuloCategoria;

namespace eAgenda.WebApi.Controllers.ModuloCategoria
{
    [ApiController]
    [Route("api/categorias")]
    public class CategoriaController : ApiControllerBase
    {
        private readonly ServicoCategoria _servicoCategoria;
        private readonly IMapper _mapeador;

        public CategoriaController(ServicoCategoria servicoCategoria, IMapper mapeador)
        {
            _servicoCategoria = servicoCategoria;
            _mapeador = mapeador;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ListarCategoriaViewModel), 200)]
        [ProducesResponseType(typeof(string[]), 500)]
        public IActionResult SelecionarTodos()
        {
            Result<List<Categoria>> resultadoBusca = _servicoCategoria.SelecionarTodos();

            if (resultadoBusca.IsFailed) return StatusNotFound(resultadoBusca);

            return ProcessarResultado(resultadoBusca, _mapeador.Map<List<ListarCategoriaViewModel>>(resultadoBusca.Value));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(FormCategoriaViewModel), 200)]
        [ProducesResponseType(typeof(string[]), 404)]
        [ProducesResponseType(typeof(string[]), 500)]
        public IActionResult SelecionarPorId(Guid id)
        {
            Result<Categoria> resultadoBusca = _servicoCategoria.SelecionarPorId(id);

            if (resultadoBusca.IsFailed) return StatusNotFound(resultadoBusca);

            return ProcessarResultado(resultadoBusca, _mapeador.Map<FormCategoriaViewModel>(resultadoBusca.Value));
        }

        [HttpGet("visualizacao-completa/{id}")]
        [ProducesResponseType(typeof(VisualizarCategoriaViewModel), 200)]
        [ProducesResponseType(typeof(string[]), 404)]
        [ProducesResponseType(typeof(string[]), 500)]
        public IActionResult SelecionarCompletoPorId(Guid id)
        {
            Result<Categoria> resultadoBusca = _servicoCategoria.SelecionarPorId(id);

            if (resultadoBusca.IsFailed) return StatusNotFound(resultadoBusca);

            return ProcessarResultado(resultadoBusca, _mapeador.Map<VisualizarCategoriaViewModel>(resultadoBusca.Value));
        }

        [HttpPost]
        [ProducesResponseType(typeof(FormCategoriaViewModel), 200)]
        [ProducesResponseType(typeof(string[]), 400)]
        [ProducesResponseType(typeof(string[]), 500)]
        public IActionResult Inserir(FormCategoriaViewModel categoriaViewModel)
        {
            Result<Categoria> resultado = _servicoCategoria.Inserir(_mapeador.Map<Categoria>(categoriaViewModel));

            return ProcessarResultado(resultado, categoriaViewModel);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(FormCategoriaViewModel), 200)]
        [ProducesResponseType(typeof(string[]), 400)]
        [ProducesResponseType(typeof(string[]), 404)]
        [ProducesResponseType(typeof(string[]), 500)]
        public IActionResult Editar(Guid id, FormCategoriaViewModel categoriaViewModel)
        {
            Result<Categoria> resultadoBusca = _servicoCategoria.SelecionarPorId(id);

            if (resultadoBusca.IsFailed) return StatusNotFound(resultadoBusca);

            Result<Categoria> resultado = _servicoCategoria.Editar(_mapeador.Map(categoriaViewModel, resultadoBusca.Value));

            return ProcessarResultado(resultado, categoriaViewModel);
        }

        [HttpDelete("{id}")]
        public IActionResult Excluir(Guid id)
        {
            Result<Categoria> resultadoBusca = _servicoCategoria.SelecionarPorId(id);

            if (resultadoBusca.IsFailed) return StatusNotFound(resultadoBusca);

            return ProcessarResultado<Categoria>(_servicoCategoria.Excluir(resultadoBusca.Value));
        }
    }
}
