using eAgenda.Aplicacao.ModuloContato;
using eAgenda.Dominio.ModuloContato;
using eAgenda.WebApi.ViewModels.ModuloContato;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace eAgenda.WebApi.Controllers.ModuloContato
{
    [ApiController]
    [Route("api/contatos")]
    public class ContatoController : ControllerBase
    {
        private readonly ServicoContato _servicoContato;
        private readonly IMapper _mapeador;

        public ContatoController(ServicoContato servicoContato, IMapper mapeador)
        {
            this._servicoContato = servicoContato;
            this._mapeador = mapeador;
        }

        [HttpGet]
        public List<ListarContatoViewModel> SelecionarTodos(StatusFavoritoEnum statusFavorito)
        {
            List<Contato> contatos = _servicoContato.SelecionarTodos(statusFavorito).Value;

            return _mapeador.Map<List<ListarContatoViewModel>>(contatos);
        }

        [HttpGet("{id}")]
        public IActionResult SelecionarPorId(Guid id)
        {            
            try
            {
                Result<Contato> resultadoBusca = _servicoContato.SelecionarPorId(id);

                if (resultadoBusca.IsFailed)
                    return NotFound(string.Join("\r\n", resultadoBusca.Errors.Select(e => e.Message).ToArray()));

                FormContatoViewModel contato = _mapeador.Map<FormContatoViewModel>(resultadoBusca);

                return Ok(contato);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("visualizacao-completa/{id}")]
        public IActionResult SelecionarCompletoPorId(Guid id)
        {
            try
            {
                Result<Contato> resultadoBusca = _servicoContato.SelecionarPorId(id);

                if (resultadoBusca.IsFailed)
                    return NotFound(string.Join("\r\n", resultadoBusca.Errors.Select(e => e.Message).ToArray()));

                VisualizarContatoViewModel contato = _mapeador.Map<VisualizarContatoViewModel>(resultadoBusca);

                return Ok(contato);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Inserir(FormContatoViewModel contatoViewModel)
        {
            try
            {
                Contato contato = _mapeador.Map<Contato>(contatoViewModel);
                Result<Contato> resultado = _servicoContato.Inserir(contato);

                return ProcessarResultado(resultado, contatoViewModel);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }        

        [HttpPut("{id}")]
        public IActionResult Editar(Guid id, FormContatoViewModel contatoViewModel)
        {
            try
            {
                Result<Contato> resultadoBusca = _servicoContato.SelecionarPorId(id);

                if (resultadoBusca.IsFailed)
                    return NotFound(string.Join("\r\n", resultadoBusca.Errors.Select(e => e.Message).ToArray()));

                // mescla os dois objs mantendo a referência do objeto destino
                Contato contato = _mapeador.Map(contatoViewModel, resultadoBusca.Value); // source, destination

                Result<Contato> resultado = _servicoContato.Editar(contato);

                return ProcessarResultado(resultado, contatoViewModel);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Excluir(Guid id)
        {
            try
            {
                Result<Contato> resultadoBusca = _servicoContato.SelecionarPorId(id);

                if (resultadoBusca.IsFailed)
                    return NotFound(string.Join("\r\n", resultadoBusca.Errors.Select(e => e.Message).ToArray()));

                Contato contato = resultadoBusca.Value;

                Result<Contato> resultado = _servicoContato.Excluir(contato);

                return ProcessarResultado(resultado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        private IActionResult ProcessarResultado(Result<Contato> resultado, FormContatoViewModel contatoViewModel = null)
        {
            if (resultado.IsFailed)
                return BadRequest(resultado.Errors.Select(e => e.Message));

            string enderecoContato = $"{Request.GetDisplayUrl()}/{resultado.Value.Id}";

            if(contatoViewModel == null)
                return Created(enderecoContato, null);

            return Created(enderecoContato, contatoViewModel);
        }
    }
}
