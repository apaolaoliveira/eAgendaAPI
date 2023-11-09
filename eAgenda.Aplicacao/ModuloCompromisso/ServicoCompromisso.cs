using eAgenda.Dominio;
using eAgenda.Dominio.ModuloCompromisso;

namespace eAgenda.Aplicacao.ModuloCompromisso
{
    public class ServicoCompromisso : ServicoApiBase<Compromisso, ValidadorCompromisso>
    {
        private IRepositorioCompromisso repositorioCompromisso;
        private IContextoPersistencia contextoPersistencia;

        public ServicoCompromisso(IRepositorioCompromisso repositorioCompromisso,
                             IContextoPersistencia contextoPersistencia)
        {
            this.repositorioCompromisso = repositorioCompromisso;
            this.contextoPersistencia = contextoPersistencia;
        }

        public Result<Compromisso> Inserir(Compromisso compromisso)
        {
            Result resultado = Validar(compromisso);

            if (resultado.IsFailed)
                return Result.Fail(resultado.Errors);

            repositorioCompromisso.Inserir(compromisso);

            contextoPersistencia.GravarDados();

            return Result.Ok(compromisso);
        }

        public Result<Compromisso> Editar(Compromisso compromisso)
        {
            var resultado = Validar(compromisso);

            if (resultado.IsFailed)
                return Result.Fail(resultado.Errors);

            repositorioCompromisso.Editar(compromisso);

            contextoPersistencia.GravarDados();

            return Result.Ok(compromisso);
        }

        public Result Excluir(Compromisso compromisso)
        {
            repositorioCompromisso.Excluir(compromisso);

            contextoPersistencia.GravarDados();

            return Result.Ok();
        }

        public Result Excluir(Guid id)
        {
            var compromissoResult = SelecionarPorId(id);

            if (compromissoResult.IsSuccess)
                return Excluir(compromissoResult.Value);

            return Result.Fail(compromissoResult.Errors);
        }

        public Result<List<Compromisso>> SelecionarTodos()
        {
            var compromissos = repositorioCompromisso.SelecionarTodos();

            return Result.Ok(compromissos);
        }

        public Result<Compromisso> SelecionarPorId(Guid id)
        {
            var compromisso = repositorioCompromisso.SelecionarPorId(id);

            if (compromisso == null)
            {
                Log.Logger.Warning("--- [Módulo Compromisso] -> Compromisso {CompromissoId} não encontrado ---", id);

                return Result.Fail("--- [Módulo Compromisso] -> Compromisso não encontrado ---");
            }

            return Result.Ok(compromisso);
        }

        public Result<List<Compromisso>> SelecionarCompromissosPassados(DateTime hoje)
        {
            return repositorioCompromisso.SelecionarCompromissosPassados(hoje);
        }

        public Result<List<Compromisso>> SelecionarCompromissosFuturos(DateTime dataInicial, DateTime dataFinal)
        {
            return repositorioCompromisso.SelecionarCompromissosFuturos(dataInicial, dataFinal);
        }
    }
}
