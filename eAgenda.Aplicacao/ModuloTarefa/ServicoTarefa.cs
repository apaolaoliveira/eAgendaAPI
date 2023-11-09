using eAgenda.Dominio;
using eAgenda.Dominio.ModuloTarefa;

namespace eAgenda.Aplicacao.ModuloTarefa
{
    public class ServicoTarefa : ServicoApiBase<Tarefa, ValidadorTarefa>
    {
        private IRepositorioTarefa repositorioTarefa;
        private IContextoPersistencia contextoPersistencia;

        public ServicoTarefa(IRepositorioTarefa repositorioTarefa,
                             IContextoPersistencia contexto)
        {
            this.repositorioTarefa = repositorioTarefa;
            this.contextoPersistencia = contexto;
        }

        public Result<Tarefa> Inserir(Tarefa tarefa)
        {
            Result resultado = Validar(tarefa);

            if (resultado.IsFailed)
                return Result.Fail(resultado.Errors);

            repositorioTarefa.Inserir(tarefa);

            contextoPersistencia.GravarDados();

            return Result.Ok(tarefa);
        }

        public Result<Tarefa> Editar(Tarefa tarefa)
        {
            var resultado = Validar(tarefa);

            if (resultado.IsFailed)
                return Result.Fail(resultado.Errors);

            tarefa.CalcularPercentualConcluido();

            repositorioTarefa.Editar(tarefa);

            contextoPersistencia.GravarDados();

            return Result.Ok(tarefa);
        }

        public Result<Tarefa> AtualizarItens(Tarefa tarefa,
            List<ItemTarefa> itensConcluidos, List<ItemTarefa> itensPendentes)
        {
            foreach (var item in itensConcluidos)
                tarefa.ConcluirItem(item.Id);

            foreach (var item in itensPendentes)
                tarefa.MarcarPendente(item.Id);

            return Editar(tarefa);
        }

        public Result Excluir(Guid id)
        {
            var tarefaResult = SelecionarPorId(id);

            if (tarefaResult.IsSuccess)
                return Excluir(tarefaResult.Value);

            return Result.Fail(tarefaResult.Errors);
        }

        public Result Excluir(Tarefa tarefa)
        {
            repositorioTarefa.Excluir(tarefa);

            contextoPersistencia.GravarDados();

            return Result.Ok();
        }

        public Result<List<Tarefa>> SelecionarTodos(StatusTarefaEnum statusSelecionado)
        {
            var tarefas = repositorioTarefa.SelecionarTodos(statusSelecionado);

            return Result.Ok(tarefas);
        }

        public Result<Tarefa> SelecionarPorId(Guid id)
        {
            var tarefa = repositorioTarefa.SelecionarPorId(id);

            if (tarefa == null)
            {
                Log.Logger.Warning("--- [Módulo Tarefa] -> Tarefa {TarefaId} não encontrada ---", id);

                return Result.Fail($"--- [Módulo Tarefa] -> Tarefa não encontrada ---");
            }

            return Result.Ok(tarefa);
        }
    }
}