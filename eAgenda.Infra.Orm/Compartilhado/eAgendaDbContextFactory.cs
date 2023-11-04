using eAgenda.Infra.Orm;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace GeradorTestes.Infra.Orm
{
    public class eAgendaDbContextFactory : IDesignTimeDbContextFactory<eAgendaDbContext>
    {
        public eAgendaDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<eAgendaDbContext>();

            builder.UseSqlServer(@"Data Source=(LOCALDB)\MSSQLLOCALDB;Initial Catalog=eAgenda;Integrated Security=True");

            return new eAgendaDbContext(builder.Options);
        }
    }
}
