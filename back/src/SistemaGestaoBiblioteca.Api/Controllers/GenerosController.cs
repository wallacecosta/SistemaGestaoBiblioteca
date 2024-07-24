using MediatR;
using Microsoft.AspNetCore.Mvc;
using SistemaGestaoBiblioteca.Application.Commands.Generos.Cadastro;

namespace SistemaGestaoBiblioteca.Api.Controllers
{
    [Route("api/generos")]
    [ApiController]
    public class GenerosController(IMediator mediator) : ControllerBase
    {
        [HttpPost("cadastrar")]
        public async Task<IActionResult> Criar([FromBody] CadastroGeneroCommand command)
        {
            return Ok(await mediator.Send(command));
        }
    }
}
