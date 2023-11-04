using eAgenda.Aplicacao.ModuloCompromisso;
using eAgenda.Dominio.ModuloCompromisso;
using eAgenda.WebApi.ViewModels.ModuloCompromisso;
using Microsoft.AspNetCore.Mvc;

namespace eAgenda.WebApi.Controllers.ModuloCompromisso
{
    [ApiController]
    [Route("api/compromissos")]
    public class CompromissoController : ControllerBase
    {
        private readonly ServicoCompromisso _servicoCompromisso;
        private readonly IMapper _mapeador;

        public CompromissoController(ServicoCompromisso servicoCompromisso, IMapper mapeador)
        {
            this._servicoCompromisso = servicoCompromisso;
            this._mapeador = mapeador;
        }

        [HttpGet]
        public List<ListarCompromissoViewModel> SelecionarTodos()
        {
            List<Compromisso> compromissos = _servicoCompromisso.SelecionarTodos().Value;

            return _mapeador.Map<List<ListarCompromissoViewModel>>(compromissos);
        }

        [HttpGet("{id}")]
        public FormCompromissoViewModel SelecionarPorId(Guid id)
        {
            Compromisso compromisso = _servicoCompromisso.SelecionarPorId(id).Value;

            return _mapeador.Map<FormCompromissoViewModel>(compromisso);
        }

        [HttpGet("visualizacao-completa/{id}")]
        public VisualizarCompromissoViewModel SelecionarCompletoPorId(Guid id)
        {
            Compromisso compromisso = _servicoCompromisso.SelecionarPorId(id).Value;

            return _mapeador.Map<VisualizarCompromissoViewModel>(compromisso);
        }

        [HttpPost]
        public string Inserir(FormCompromissoViewModel compromissoViewModel)
        {
            Compromisso compromisso = _mapeador.Map<Compromisso>(compromissoViewModel);

            Result<Compromisso> resultado = _servicoCompromisso.Inserir(compromisso);

            return processarResultado(resultado, "inserido");
        }

        [HttpPut("{id}")]
        public string Editar(Guid id, FormCompromissoViewModel compromissoViewModel)
        {
            Result<Compromisso> resultadoBusca = _servicoCompromisso.SelecionarPorId(id);

            if (resultadoBusca.IsFailed)
                return string.Join("/r/n", resultadoBusca.Errors.Select(e => e.Message).ToArray());

            Compromisso compromisso = _mapeador.Map(compromissoViewModel, resultadoBusca.Value);

            Result<Compromisso> resultado = _servicoCompromisso.Editar(compromisso);

            return processarResultado(resultado, "editado");
        }

        [HttpDelete("{id}")]
        public string Excluir(Guid id)
        {
            Result<Compromisso> resultadoBusca = _servicoCompromisso.SelecionarPorId(id);

            if (resultadoBusca.IsFailed)
                return string.Join("/r/n", resultadoBusca.Errors.Select(e => e.Message).ToArray());

            Compromisso compromisso = resultadoBusca.Value;

            Result<Compromisso> resultado = _servicoCompromisso.Excluir(compromisso);

            return processarResultado(resultado, "removido");
        }

        private string processarResultado(Result<Compromisso> resultado, string acao)
        {
            if (resultado.IsSuccess)
                return $"Compromisso {acao} com sucesso!";

            string[] erros = resultado.Errors.Select(x => x.Message).ToArray();

            return string.Join("\r\n", erros);
        }
    }
}
