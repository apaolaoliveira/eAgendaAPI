using Microsoft.AspNetCore.Mvc;

namespace eAgenda.WebApi.Controllers.Compartilhado
{
    public abstract class ApiControllerBase : ControllerBase
    {
        protected IActionResult ProcessarResultado<Type>(Result<Type> resposta, object obj = null)
        {
            if (resposta.IsFailed)
                return BadRequest(string.Join("\r\n", resposta.Errors.Select(e => e.Message).ToArray()));

            return StatusOk(obj);
        }

        protected IActionResult StatusOk(object obj)
        {
            return Ok(new
            {
                sucesso = true,
                dados = obj
            });
        }

        protected IActionResult StatusNotFound<Type>(Result<Type> resposta)
        {
            return NotFound(new
            {
                sucesso = false,
                erros = string.Join("\r\n", resposta.Errors.Select(e => e.Message).ToArray())
            });
        }
    }
}
