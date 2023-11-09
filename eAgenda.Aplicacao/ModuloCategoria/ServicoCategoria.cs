using eAgenda.Dominio;
using eAgenda.Dominio.ModuloCategoria;

namespace eAgenda.Aplicacao.ModuloCategoria
{
    public class ServicoCategoria : ServicoApiBase<Categoria, ValidadorCategoria>
    {
        private IRepositorioCategoria repositorioCategoria;
        private IContextoPersistencia contextoPersistencia;

        public ServicoCategoria(IRepositorioCategoria repositorioCategoria,
                             IContextoPersistencia contexto)
        {
            this.repositorioCategoria = repositorioCategoria;
            contextoPersistencia = contexto;
        }

        public Result<Categoria> Inserir(Categoria categoria)
        {
            Result resultado = Validar(categoria);

            if (resultado.IsFailed)
                return Result.Fail(resultado.Errors);

            repositorioCategoria.Inserir(categoria);

            contextoPersistencia.GravarDados();

            return Result.Ok(categoria);
        }

        public Result<Categoria> Editar(Categoria categoria)
        {
            var resultado = Validar(categoria);

            if (resultado.IsFailed)
                return Result.Fail(resultado.Errors);

            repositorioCategoria.Editar(categoria);

            contextoPersistencia.GravarDados();

            return Result.Ok(categoria);
        }

        public Result Excluir(Guid id)
        {
            var categoriaResult = SelecionarPorId(id);

            if (categoriaResult.IsSuccess)
                return Excluir(categoriaResult.Value);

            return Result.Fail(categoriaResult.Errors);
        }

        public Result Excluir(Categoria categoria)
        {
            repositorioCategoria.Excluir(categoria);

            contextoPersistencia.GravarDados();

            return Result.Ok();
        }

        public Result<List<Categoria>> SelecionarTodos()
        {
            var categorias = repositorioCategoria.SelecionarTodos();

            return Result.Ok(categorias);
        }

        public Result<Categoria> SelecionarPorId(Guid id)
        {
            var categoria = repositorioCategoria.SelecionarPorId(id);

            if (categoria == null)
            {
                Log.Logger.Warning("--- [Módulo Categoria] -> Categoria {CategoriaId} não encontrada ---", id);

                return Result.Fail("--- [Módulo Categoria] -> Categoria não encontrada ---");
            }

            return Result.Ok(categoria);
        }
    }
}
