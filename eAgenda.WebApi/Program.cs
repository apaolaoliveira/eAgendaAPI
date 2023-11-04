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
            
            // Extens�es de configura��es feitas na pasta Configs
            //builder.Services.AddAutoMapper(typeof(Program).Assembly); -> esse � um modo mais gen�rico de gerar depend�ncia
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