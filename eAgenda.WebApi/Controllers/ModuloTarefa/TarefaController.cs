using eAgenda.Aplicacao.ModuloTarefa;
using eAgenda.Dominio.ModuloTarefa;
using eAgenda.WebApi.ViewModels.ModuloTarefa;
using Microsoft.AspNetCore.Mvc;

namespace eAgenda.WebApi.Controllers.ModuloTarefa
{
    [ApiController]
    [Route("api/tarefas")]
    public class TarefaController : Controller
    {
        private readonly ServicoTarefa _servicoTarefa;
        private readonly IMapper _mapeador;

        public TarefaController(ServicoTarefa servicoTarefa, IMapper mapeador)
        {
            _servicoTarefa = servicoTarefa;
            _mapeador = mapeador;
        }

        [HttpGet]
        public List<ListarTarefaViewModel> SelecionarTodos(StatusTarefaEnum statusSelecionado)
        {
            List<Tarefa> Tarefas = _servicoTarefa.SelecionarTodos(statusSelecionado).Value;

            return _mapeador.Map<List<ListarTarefaViewModel>>(Tarefas);
        }

        [HttpGet("{id}")]
        public FormTarefaViewModel SelecionarPorId(Guid id)
        {
            Tarefa tarefa = _servicoTarefa.SelecionarPorId(id).Value;

            return _mapeador.Map<FormTarefaViewModel>(tarefa);
        }

        [HttpGet("visualizacao-completa/{id}")]
        public VisualizarTarefaViewModel SelecionarCompletoPorId(Guid id)
        {
            Tarefa tarefa = _servicoTarefa.SelecionarPorId(id).Value;

            return _mapeador.Map<VisualizarTarefaViewModel>(tarefa);
        }

        [HttpPost]
        public string Inserir(FormTarefaViewModel tarefaViewModel)
        {
            Tarefa tarefa = _mapeador.Map<Tarefa>(tarefaViewModel);

            Result<Tarefa> resultado = _servicoTarefa.Inserir(tarefa);

            return processarResultado(resultado, "inserido");
        }

        [HttpPut("{id}")]
        public string Editar(Guid id, FormTarefaViewModel tarefaViewModel)
        {
            Result<Tarefa> resultadoBusca = _servicoTarefa.SelecionarPorId(id);

            if (resultadoBusca.IsFailed)
                return string.Join("/r/n", resultadoBusca.Errors.Select(e => e.Message).ToArray());

            Tarefa tarefa = _mapeador.Map(tarefaViewModel, resultadoBusca.Value);

            Result<Tarefa> resultado = _servicoTarefa.Editar(tarefa);

            return processarResultado(resultado, "editado");
        }

        [HttpDelete("{id}")]
        public string Excluir(Guid id)
        {
            Result<Tarefa> resultadoBusca = _servicoTarefa.SelecionarPorId(id);

            if (resultadoBusca.IsFailed)
                return string.Join("/r/n", resultadoBusca.Errors.Select(e => e.Message).ToArray());

            Tarefa tarefa = resultadoBusca.Value;

            Result<Tarefa> resultado = _servicoTarefa.Excluir(tarefa);

            return processarResultado(resultado, "removido");
        }

        private string processarResultado(Result<Tarefa> resultado, string acao)
        {
            if (resultado.IsSuccess)
                return $"Tarefa {acao} com sucesso!";

            string[] erros = resultado.Errors.Select(x => x.Message).ToArray();

            return string.Join("\r\n", erros);
        }
    }
}
