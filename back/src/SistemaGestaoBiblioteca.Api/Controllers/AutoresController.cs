using MediatR;
using Microsoft.AspNetCore.Mvc;
using SistemaGestaoBiblioteca.Application.Commands.Autores.CadastroDeAutor;

namespace SistemaGestaoBiblioteca.Api.Controllers
{
    [Route("api/autores")]
    [ApiController]
    public class AutoresController(IMediator mediator) : ControllerBase
    {
        [HttpPost("cadastrar")]
        public async Task<IActionResult> Criar([FromBody] CadastroAutorCommand command)
        {
            return Ok(await mediator.Send(command));
        }
    }
}
