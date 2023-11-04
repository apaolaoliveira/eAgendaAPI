using eAgenda.Aplicacao.ModuloContato;
using eAgenda.Dominio.ModuloContato;
using eAgenda.WebApi.ViewModels.ModuloContato;
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
        public FormContatoViewModel SelecionarPorId(Guid id)
        {
            Contato contato = _servicoContato.SelecionarPorId(id).Value;

            return _mapeador.Map<FormContatoViewModel>(contato);
        }

        [HttpGet("visualizacao-completa/{id}")]
        public VisualizarContatoViewModel SelecionarCompletoPorId(Guid id)
        {
            Contato contato = _servicoContato.SelecionarPorId(id).Value;

            return _mapeador.Map<VisualizarContatoViewModel>(contato);
        }

        [HttpPost]
        public string Inserir(FormContatoViewModel contatoViewModel)
        {
            Contato contato = _mapeador.Map<Contato>(contatoViewModel);

            Result<Contato> resultado = _servicoContato.Inserir(contato);

            return processarResultado(resultado, "inserido");
        }

        [HttpPut("{id}")]
        public string Editar(Guid id, FormContatoViewModel contatoViewModel)
        {
            Result<Contato> resultadoBusca = _servicoContato.SelecionarPorId(id);

            if (resultadoBusca.IsFailed)
                return string.Join("/r/n", resultadoBusca.Errors.Select(e => e.Message).ToArray());

            // mescla os dois objs mantendo a referência do objeto destino
            Contato contato = _mapeador.Map(contatoViewModel, resultadoBusca.Value); // source, destination

            Result<Contato> resultado = _servicoContato.Editar(contato);

            return processarResultado(resultado, "editado");
        }

        [HttpDelete("{id}")]
        public string Excluir(Guid id)
        {
            Result<Contato> resultadoBusca = _servicoContato.SelecionarPorId(id);

            if (resultadoBusca.IsFailed)
                return string.Join("/r/n", resultadoBusca.Errors.Select(e => e.Message).ToArray());            

            Contato contato = resultadoBusca.Value;

            Result<Contato> resultado = _servicoContato.Excluir(contato);

            return processarResultado(resultado, "removido");
        }

        private string processarResultado(Result<Contato> resultado, string acao)
        {
            if (resultado.IsSuccess)
                return $"Contato {acao} com sucesso!";

            string[] erros = resultado.Errors.Select(x => x.Message).ToArray();

            return string.Join("\r\n", erros);
        }
    }
}
