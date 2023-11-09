using Serilog;

namespace eAgenda.WebApi.Configs.Serilog
{
    public static class SerilogConfigExtension
    {
        public static void ConfigurarSerilog(this IServiceCollection services, ILoggingBuilder logging)
        {
            logging.ClearProviders();

            Log.Logger = new LoggerConfiguration()
              .MinimumLevel.Information()
              .Enrich.FromLogContext()
              .WriteTo.Console()
              .CreateLogger();

            Log.Logger.Information("--- Bem vindo ao e-Agenda Api ---");

            services.AddSerilog(Log.Logger);
        }
    }
}
