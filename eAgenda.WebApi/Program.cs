using eAgenda.WebApi.Compartilhado;
using eAgenda.WebApi.Configs.AutoMapperProfiles;
using eAgenda.WebApi.Configs.InjecaoDependencia;
using eAgenda.WebApi.Configs.Serilog;
using eAgenda.WebApi.Configs.Swagger;
using eAgenda.WebApi.Filtros;
using Microsoft.AspNetCore.Mvc;

namespace eAgenda.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

            builder.Services.Configure<ApiBehaviorOptions>(config =>
            {
                config.SuppressModelStateInvalidFilter = true;
            });

            builder.Services.ConfigurarSerilog(builder.Logging); 
            builder.Services.ConfigurarInjecaoDependencia(builder.Configuration);
            builder.Services.ConfigurarAutoMapper();
            builder.Services.ConfigurarSwagger();

            builder.Services.AddControllers(config =>
            {
                config.Filters.Add<SerilogActionFilter>();
            });

            WebApplication app = builder.Build();

            app.UseMiddleware<ManipuladorExcecoes>();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}