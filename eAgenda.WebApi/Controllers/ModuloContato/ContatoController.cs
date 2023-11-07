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
        private readonly ILogger<ContatoController> _logger;    

        public ContatoController(ServicoContato servicoContato, IMapper mapeador, ILogger<ContatoController> logger)
        {
            this._servicoContato = servicoContato;
            this._mapeador = mapeador;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ListarContatoViewModel), 200)]
        [ProducesResponseType(typeof(string[]), 500)]
        public IActionResult SelecionarTodos(StatusFavoritoEnum statusFavorito)
        {
            this._logger.LogInformation("Selecionando todos os contatos");

            List<Contato> contatos = _servicoContato.SelecionarTodos(statusFavorito).Value;

            return Ok(new
            {
                Sucesso = true,
                Dados = _mapeador.Map<List<ListarContatoViewModel>>(contatos),
                QtdRegistros = contatos.Count
            });
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(VisualizarContatoViewModel), 200)]
        [ProducesResponseType(typeof(string[]), 404)]
        [ProducesResponseType(typeof(string[]), 500)]
        public IActionResult SelecionarPorId(Guid id)
        {
            this._logger.LogInformation($"Selecionando o contato com o id: {id}");

            Result<Contato> resultadoBusca = _servicoContato.SelecionarPorId(id);

            if (resultadoBusca.IsFailed)
                return NotFound(new
                {
                    Sucesso = false,
                    Erros = string.Join("\r\n", resultadoBusca.Errors.Select(e => e.Message).ToArray())
                });

            return Ok(new
            {
                Sucesso = true,
                Dados = _mapeador.Map<FormContatoViewModel>(resultadoBusca)
            });
        }

        [HttpGet("visualizacao-completa/{id}")]
        [ProducesResponseType(typeof(VisualizarContatoViewModel), 200)]
        [ProducesResponseType(typeof(string[]), 404)]
        [ProducesResponseType(typeof(string[]), 500)]
        public IActionResult SelecionarCompletoPorId(Guid id)
        {
            this._logger.LogInformation($"Selecionando todas as info do contato com o id: {id}");

            Result<Contato> resultadoBusca = _servicoContato.SelecionarPorId(id);

            if (resultadoBusca.IsFailed)
                return NotFound(new
                {
                    Sucesso = false,
                    Erros = string.Join("\r\n", resultadoBusca.Errors.Select(e => e.Message).ToArray())
                });

            return Ok(new
            {
                Sucesso = true,
                Dados = _mapeador.Map<VisualizarContatoViewModel>(resultadoBusca)
            });
        }

        [HttpPost]
        [ProducesResponseType(typeof(FormContatoViewModel), 200)]
        [ProducesResponseType(typeof(string[]), 400)]
        [ProducesResponseType(typeof(string[]), 500)]
        public IActionResult Inserir(FormContatoViewModel contatoViewModel)
        {
            this._logger.LogInformation($"Inserindo contato");

            Contato contato = _mapeador.Map<Contato>(contatoViewModel);

            return ProcessarResultado(_servicoContato.Inserir(contato), contatoViewModel);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(FormContatoViewModel), 200)]
        [ProducesResponseType(typeof(string[]), 400)]
        [ProducesResponseType(typeof(string[]), 404)]
        [ProducesResponseType(typeof(string[]), 500)]
        public IActionResult Editar(Guid id, FormContatoViewModel contatoViewModel)
        {
            this._logger.LogInformation($"Editando contato");

            Result<Contato> resultadoBusca = _servicoContato.SelecionarPorId(id);

            if (resultadoBusca.IsFailed)
                return NotFound(new
                {
                    Sucesso = false,
                    Erros = string.Join("\r\n", resultadoBusca.Errors.Select(e => e.Message).ToArray())
                });

            // mescla os dois objs mantendo a referência do objeto destino
            Contato contato = _mapeador.Map(contatoViewModel, resultadoBusca.Value); // source, destination

            return ProcessarResultado(_servicoContato.Editar(contato), contatoViewModel);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(string[]), 400)]
        [ProducesResponseType(typeof(string[]), 404)]
        [ProducesResponseType(typeof(string[]), 500)]
        public IActionResult Excluir(Guid id)
        {
            this._logger.LogInformation($"Excluindo contato");

            Result<Contato> resultadoBusca = _servicoContato.SelecionarPorId(id);

            if (resultadoBusca.IsFailed)
                return NotFound(new
                {
                    Sucesso = false,
                    Erros = string.Join("\r\n", resultadoBusca.Errors.Select(e => e.Message).ToArray())
                });

            return ProcessarResultado(_servicoContato.Excluir(resultadoBusca.Value));
        }

        private IActionResult ProcessarResultado(Result<Contato> resultadoBusca, FormContatoViewModel contatoViewModel = null)
        {
            if (resultadoBusca.IsFailed)
                return NotFound(new
                {
                    sucesso = false,
                    erros = resultadoBusca.Errors.Select(e => e.Message)
                });

            return Ok(new
            {
                Sucesso = true,
                Dados = contatoViewModel
            });
        }
    }
}
