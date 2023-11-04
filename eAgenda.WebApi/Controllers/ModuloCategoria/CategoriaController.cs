using eAgenda.Aplicacao.ModuloCategoria;
using eAgenda.Dominio.ModuloCategoria;
using eAgenda.WebApi.ViewModels.ModuloCategoria;
using Microsoft.AspNetCore.Mvc;

namespace eAgenda.WebApi.Controllers.ModuloCategoria
{
    [ApiController]
    [Route("api/categorias")]
    public class CategoriaController : Controller
    {
        private readonly ServicoCategoria _servicoCategoria;
        private readonly IMapper _mapeador;

        public CategoriaController(ServicoCategoria servicoCategoria, IMapper mapeador)
        {
            _servicoCategoria = servicoCategoria;
            _mapeador = mapeador;
        }

        [HttpGet]
        public List<ListarCategoriaViewModel> SelecionarTodos()
        {
            List<Categoria> categorias = _servicoCategoria.SelecionarTodos().Value;

            return _mapeador.Map<List<ListarCategoriaViewModel>>(categorias);
        }

        [HttpGet("{id}")]
        public FormCategoriaViewModel SelecionarPorId(Guid id)
        {
            Categoria categoria = _servicoCategoria.SelecionarPorId(id).Value;

            return _mapeador.Map<FormCategoriaViewModel>(categoria);
        }

        [HttpGet("visualizacao-completa/{id}")]
        public VisualizarCategoriaViewModel SelecionarCompletoPorId(Guid id)
        {
            Categoria categoria = _servicoCategoria.SelecionarPorId(id).Value;

            return _mapeador.Map<VisualizarCategoriaViewModel>(categoria);
        }

        [HttpPost]
        public string Inserir(FormCategoriaViewModel categoriaViewModel)
        {
            Categoria categoria = _mapeador.Map<Categoria>(categoriaViewModel);

            Result<Categoria> resultado = _servicoCategoria.Inserir(categoria);

            return processarResultado(resultado, "inserido");
        }

        [HttpPut("{id}")]
        public string Editar(Guid id, FormCategoriaViewModel categoriaViewModel)
        {
            Result<Categoria> resultadoBusca = _servicoCategoria.SelecionarPorId(id);

            if (resultadoBusca.IsFailed)
                return string.Join("/r/n", resultadoBusca.Errors.Select(e => e.Message).ToArray());

            Categoria categoria = _mapeador.Map(categoriaViewModel, resultadoBusca.Value); 

            Result<Categoria> resultado = _servicoCategoria.Editar(categoria);

            return processarResultado(resultado, "editado");
        }

        [HttpDelete("{id}")]
        public string Excluir(Guid id)
        {
            Result<Categoria> resultadoBusca = _servicoCategoria.SelecionarPorId(id);

            if (resultadoBusca.IsFailed)
                return string.Join("/r/n", resultadoBusca.Errors.Select(e => e.Message).ToArray());

            Categoria categoria = resultadoBusca.Value;

            Result<Categoria> resultado = _servicoCategoria.Excluir(categoria);

            return processarResultado(resultado, "removido");
        }

        private string processarResultado(Result<Categoria> resultado, string acao)
        {
            if (resultado.IsSuccess)
                return $"Categoria {acao} com sucesso!";

            string[] erros = resultado.Errors.Select(x => x.Message).ToArray();

            return string.Join("\r\n", erros);
        }
    }
}
