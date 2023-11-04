using eAgenda.WebApi.Configs.AutoMapperProfiles.ModuloCategoria;
using eAgenda.WebApi.Configs.AutoMapperProfiles.ModuloCompromisso;
using eAgenda.WebApi.Configs.AutoMapperProfiles.ModuloContato;
using eAgenda.WebApi.Configs.AutoMapperProfiles.ModuloDespesas;
using eAgenda.WebApi.Configs.AutoMapperProfiles.ModuloTarefas;

namespace eAgenda.WebApi.Configs.AutoMapperProfiles
{
    public static class AutoMapperExtension
    {
        public static void ConfigurarAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(opt =>
            {
                opt.AddProfile<CategoriaProfile>();
                opt.AddProfile<CompromissoProfile>();
                opt.AddProfile<ContatoProfile>();
                opt.AddProfile<DespesaProfile>();
                opt.AddProfile<TarefaProfile>();
            });
        }
    }
}
