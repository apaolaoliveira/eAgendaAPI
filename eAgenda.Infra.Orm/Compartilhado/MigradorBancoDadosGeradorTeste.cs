using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace eAgenda.Infra.Orm
{
    public static class MigradorBancoDadoseAgenda
    {
        public static bool AtualizarBancoDados(DbContext db)
        {
            var qtdMigracoesPendentes = db.Database.GetPendingMigrations().Count();

            if (qtdMigracoesPendentes == 0)
                return false;

            db.Database.Migrate();

            return true;
        }
    }
}
