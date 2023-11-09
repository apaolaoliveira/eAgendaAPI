using eAgenda.Dominio;
using eAgenda.Dominio.ModuloDespesa;

namespace eAgenda.Aplicacao.ModuloDespesa
{
    public class ServicoDespesa : ServicoApiBase<Despesa, ValidadorDespesa>
    {
        private IRepositorioDespesa repositorioDespesa;
        private IContextoPersistencia contextoPersistencia;

        public ServicoDespesa(IRepositorioDespesa repositorioDespesa,
                             IContextoPersistencia contexto)
        {
            this.repositorioDespesa = repositorioDespesa;
            this.contextoPersistencia = contexto;
        }

        public Result<Despesa> Inserir(Despesa despesa)
        {
            Result resultado = Validar(despesa);

            if (resultado.IsFailed)
                return Result.Fail(resultado.Errors);

            repositorioDespesa.Inserir(despesa);

            contextoPersistencia.GravarDados();

            return Result.Ok(despesa);
        }

        public Result<Despesa> Editar(Despesa despesa)
        {
            var resultado = Validar(despesa);

            if (resultado.IsFailed)
                return Result.Fail(resultado.Errors);

            repositorioDespesa.Editar(despesa);

            contextoPersistencia.GravarDados();

            return Result.Ok(despesa);
        }

        public Result Excluir(Despesa despesa)
        {
            repositorioDespesa.Excluir(despesa);

            contextoPersistencia.GravarDados();

            return Result.Ok();
        }

        public Result Excluir(Guid id)
        {
            var despesaResult = SelecionarPorId(id);

            if (despesaResult.IsSuccess)
                return Excluir(despesaResult.Value);

            return Result.Fail(despesaResult.Errors);
        }

        public Result<List<Despesa>> SelecionarTodos()
        {
            var despesas = repositorioDespesa.SelecionarTodos();

            return Result.Ok(despesas);
        }

        public Result<List<Despesa>> SelecionarDespesasAntigas(DateTime dataAtual)
        {
            var despesas = repositorioDespesa.SelecionarDespesasAntigas(dataAtual);

            return Result.Ok(despesas);
        }

        public Result<List<Despesa>> SelecionarDespesasUltimos30Dias(DateTime dataAtual)
        {
            var despesas = repositorioDespesa.SelecionarDespesasUltimos30Dias(dataAtual);

            return Result.Ok(despesas);
        }

        public Result<Despesa> SelecionarPorId(Guid id)
        {
            var despesa = repositorioDespesa.SelecionarPorId(id);

            if (despesa == null)
            {
                Log.Logger.Warning("--- [Módulo Despesa] -> Despesa {DespesaId} não encontrada ---", id);

                return Result.Fail("--- [Módulo Despesa] -> Despesa não encontrada ---");
            }

            return Result.Ok(despesa);
        }
    }
}