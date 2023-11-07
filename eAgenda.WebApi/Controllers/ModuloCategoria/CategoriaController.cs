using eAgenda.Aplicacao.ModuloCategoria;
using eAgenda.Dominio.ModuloCategoria;
using eAgenda.WebApi.Controllers.ModuloContato;
using eAgenda.WebApi.ViewModels.ModuloCategoria;
using Microsoft.AspNetCore.Mvc;

namespace eAgenda.WebApi.Controllers.ModuloCategoria
{
    [ApiController]
    [Route("api/categorias")]
    public class CategoriaController : ApiControllerBase<FormCategoriaViewModel, ListarCategoriaViewModel, VisualizarCategoriaViewModel, Categoria, CategoriaController>
    {
        public CategoriaController(ServicoCategoria servicoCategoria, IMapper map, ILogger<CategoriaController> logger) : base(servicoCategoria, map, logger)
        {
            entidade = "categoria";
        }     
    }
}
