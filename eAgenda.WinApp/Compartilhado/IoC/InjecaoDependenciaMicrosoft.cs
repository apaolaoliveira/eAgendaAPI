using eAgenda.Aplicacao.ModuloCategoria;
using eAgenda.Aplicacao.ModuloCompromisso;
using eAgenda.Aplicacao.ModuloContato;
using eAgenda.Aplicacao.ModuloDespesa;
using eAgenda.Aplicacao.ModuloTarefa;

using eAgenda.Dominio;
using eAgenda.Dominio.ModuloCategoria;
using eAgenda.Dominio.ModuloCompromisso;
using eAgenda.Dominio.ModuloContato;
using eAgenda.Dominio.ModuloDespesa;
using eAgenda.Dominio.ModuloTarefa;

using eAgenda.Infra.Orm;
using eAgenda.Infra.Orm.ModuloCategoria;
using eAgenda.Infra.Orm.ModuloCompromisso;
using eAgenda.Infra.Orm.ModuloContato;
using eAgenda.Infra.Orm.ModuloDespesa;
using eAgenda.Infra.Orm.ModuloTarefa;
using eAgenda.WinApp.ModuloCategoria;
using eAgenda.WinApp.ModuloCompromisso;
using eAgenda.WinApp.ModuloContato;
using eAgenda.WinApp.ModuloDespesa;
using eAgenda.WinApp.ModuloTarefa;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;

namespace eAgenda.WinApp.Compartilhado.Ioc
{
    public class InjecaoDependenciaMicrosoft : InjecaoDependencia
    {
        private ServiceProvider container;
        public InjecaoDependenciaMicrosoft()
        {
            IConfiguration configuracao = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json")
               .Build();

            var connectionString = configuracao.GetConnectionString("SqlServer");

            var servicos = new ServiceCollection();

            servicos.AddDbContext<IContextoPersistencia, eAgendaDbContext>(optionsBuilder =>
            {
                optionsBuilder.UseSqlServer(connectionString);
            });

            servicos.AddTransient<IRepositorioTarefa, RepositorioTarefaOrm>();
            servicos.AddTransient<ServicoTarefa>();
            servicos.AddTransient<ControladorTarefa>();

            servicos.AddTransient<IRepositorioContato, RepositorioContatoOrm>();
            servicos.AddTransient<ServicoContato>();
            servicos.AddTransient<ControladorContato>();

            servicos.AddTransient<IRepositorioCompromisso, RepositorioCompromissoOrm>();
            servicos.AddTransient<ServicoCompromisso>();
            servicos.AddTransient<ControladorCompromisso>();

            servicos.AddTransient<IRepositorioDespesa, RepositorioDespesaOrm>();
            servicos.AddTransient<ServicoDespesa>();
            servicos.AddTransient<ControladorDespesa>();

            servicos.AddTransient<IRepositorioCategoria, RepositorioCategoriaOrm>();
            servicos.AddTransient<ServicoCategoria>();
            servicos.AddTransient<ControladorCategoria>();

            container = servicos.BuildServiceProvider();
        }

        public T Get<T>()
        {
            return container.GetService<T>();
        }
    }
}
