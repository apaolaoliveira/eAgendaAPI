using eAgenda.Aplicacao.ModuloContato;
using eAgenda.Dominio.ModuloContato;
using eAgenda.Dominio;
using eAgenda.Infra.Orm.ModuloContato;
using eAgenda.Infra.Orm;
using Microsoft.EntityFrameworkCore;
using eAgenda.Dominio.ModuloTarefa;
using eAgenda.Infra.Orm.ModuloTarefa;
using eAgenda.Aplicacao.ModuloTarefa;
using eAgenda.Dominio.ModuloCompromisso;
using eAgenda.Infra.Orm.ModuloCompromisso;
using eAgenda.Aplicacao.ModuloCompromisso;
using eAgenda.Dominio.ModuloDespesa;
using eAgenda.Infra.Orm.ModuloDespesa;
using eAgenda.Aplicacao.ModuloDespesa;
using eAgenda.Dominio.ModuloCategoria;
using eAgenda.Infra.Orm.ModuloCategoria;
using eAgenda.Aplicacao.ModuloCategoria;

namespace eAgenda.WebApi.Configs.InjecaoDependencia
{
    public static class InjecaoDependenciaConfigExtension
    {
        public static void ConfigurarInjecaoDependencia(this IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("SqlServer");

            services.AddDbContext<IContextoPersistencia, eAgendaDbContext>(optionsBuilder =>
            {
                optionsBuilder.UseSqlServer(connectionString);
            });

            services.AddTransient<IRepositorioContato, RepositorioContatoOrm>();
            services.AddTransient<ServicoContato>();

            services.AddTransient<IRepositorioTarefa, RepositorioTarefaOrm>();
            services.AddTransient<ServicoTarefa>();

            services.AddTransient<IRepositorioCompromisso, RepositorioCompromissoOrm>();
            services.AddTransient<ServicoCompromisso>();

            services.AddTransient<IRepositorioDespesa, RepositorioDespesaOrm>();
            services.AddTransient<ServicoDespesa>();

            services.AddTransient<IRepositorioCategoria, RepositorioCategoriaOrm>();
            services.AddTransient<ServicoCategoria>();
        }
    }
}
