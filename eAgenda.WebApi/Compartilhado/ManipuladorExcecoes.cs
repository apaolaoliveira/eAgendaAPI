using Serilog;
using System.Text.Json;

namespace eAgenda.WebApi.Compartilhado
{
    public class ManipuladorExcecoes
    {
        private readonly RequestDelegate _requestDelegate;
        public ManipuladorExcecoes(RequestDelegate requestDelegate)
        {
            _requestDelegate = requestDelegate;
        }

        public async Task Invoke(HttpContext ctx)
        {
            try
            {
                await this._requestDelegate(ctx); 
            }
            catch (Exception ex)
            {
                ctx.Response.StatusCode = 500;
                ctx.Response.ContentType = "application/json";

                var retorno = new {
                    sucesso = false,
                    erros = new List<string>() { ex.Message }
                };

                Log.Logger.Error(ex, ex.Message);

                ctx.Response.WriteAsync(JsonSerializer.Serialize(retorno));
            }
        }
    }
}
