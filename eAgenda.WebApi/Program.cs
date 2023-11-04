using eAgenda.WebApi.Configs.AutoMapperProfiles;
using eAgenda.WebApi.Configs.InjecaoDependencia;
using eAgenda.WebApi.Configs.Swagger;

namespace eAgenda.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
            
            // Extensões de configurações feitas na pasta Configs
            //builder.Services.AddAutoMapper(typeof(Program).Assembly); -> esse é um modo mais genérico de gerar dependência
            builder.Services.ConfigurarInjecaoDependencia(builder.Configuration);
            builder.Services.ConfigurarAutoMapper();
            builder.Services.ConfigurarSwagger();

            builder.Services.AddControllers();

            WebApplication app = builder.Build();

            // Configure the HTTP request pipeline.
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