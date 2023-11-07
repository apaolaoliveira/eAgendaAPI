using eAgenda.Aplicacao.ModuloDespesa;
using eAgenda.Dominio.ModuloDespesa;
using eAgenda.WebApi.ViewModels.ModuloDespesa;
using Microsoft.AspNetCore.Mvc;

namespace eAgenda.WebApi.Controllers.ModuloDespesa
{
    [ApiController]
    [Route("api/despesas")]
    public class DespesaController : ControllerBase
    {
        private readonly ServicoDespesa _servicoDespesa;
        private readonly IMapper _mapeador;

        public DespesaController(ServicoDespesa servicoDespesa, IMapper mapeador)
        {
            _servicoDespesa = servicoDespesa;
            _mapeador = mapeador;
        }

        [HttpGet]
        public List<ListarDespesaViewModel> SelecionarTodos()
        {
            List<Despesa> despesas = _servicoDespesa.SelecionarTodos().Value;

            return _mapeador.Map<List<ListarDespesaViewModel>>(despesas);
        }

        [HttpGet("{id}")]
        public FormDespesaViewModel SelecionarPorId(Guid id)
        {
            Despesa despesa = _servicoDespesa.SelecionarPorId(id).Value;

            return _mapeador.Map<FormDespesaViewModel>(despesa);
        }

        [HttpGet("visualizacao-completa/{id}")]
        public VisualizarDespesaViewModel SelecionarCompletoPorId(Guid id)
        {
            Despesa despesa = _servicoDespesa.SelecionarPorId(id).Value;

            return _mapeador.Map<VisualizarDespesaViewModel>(despesa);
        }

        [HttpPost]
        public string Inserir(FormDespesaViewModel despesaViewModel)
        {
            Despesa despesa = _mapeador.Map<Despesa>(despesaViewModel);

            Result<Despesa> resultado = _servicoDespesa.Inserir(despesa);

            return processarResultado(resultado, "inserido");
        }

        [HttpPut("{id}")]
        public string Editar(Guid id, FormDespesaViewModel despesaViewModel)
        {
            Result<Despesa> resultadoBusca = _servicoDespesa.SelecionarPorId(id);

            if (resultadoBusca.IsFailed)
                return string.Join("/r/n", resultadoBusca.Errors.Select(e => e.Message).ToArray());

            Despesa despesa = _mapeador.Map(despesaViewModel, resultadoBusca.Value);

            Result<Despesa> resultado = _servicoDespesa.Editar(despesa);

            return processarResultado(resultado, "editado");
        }

        [HttpDelete("{id}")]
        public string Excluir(Guid id)
        {
            Result<Despesa> resultadoBusca = _servicoDespesa.SelecionarPorId(id);

            if (resultadoBusca.IsFailed)
                return string.Join("/r/n", resultadoBusca.Errors.Select(e => e.Message).ToArray());

            Despesa despesa = resultadoBusca.Value;

            Result<Despesa> resultado = _servicoDespesa.Excluir(despesa);

            return processarResultado(resultado, "removido");
        }

        private string processarResultado(Result<Despesa> resultado, string acao)
        {
            if (resultado.IsSuccess)
                return $"Despesa {acao} com sucesso!";

            string[] erros = resultado.Errors.Select(x => x.Message).ToArray();

            return string.Join("\r\n", erros);
        }
    }
}
