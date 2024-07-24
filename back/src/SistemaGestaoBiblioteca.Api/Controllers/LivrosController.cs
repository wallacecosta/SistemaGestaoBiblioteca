using MediatR;
using Microsoft.AspNetCore.Mvc;
using SistemaGestaoBiblioteca.Application.Commands.Livros.Alteracao;
using SistemaGestaoBiblioteca.Application.Commands.Livros.Cadastro;
using SistemaGestaoBiblioteca.Application.Commands.Livros.Exclusao;

namespace SistemaGestaoBiblioteca.Api.Controllers
{
    [Route("api/livros")]
    [ApiController]
    public class LivrosController(IMediator mediator) : ControllerBase
    {
        [HttpPost("cadastrar")]
        public async Task<IActionResult> Criar([FromBody] CadastroLivroCommand command)
        {
            return Ok(await mediator.Send(command));
        }

        [HttpPut("atualizar")]
        public async Task<IActionResult> Atualizar([FromBody] AlteracaoLivroCommand command)
        {
            return Ok(await mediator.Send(command));
        }

        [HttpDelete("{id}/excluir")]
        public async Task<IActionResult> Excluir(Guid id)
        {
            return Ok(await mediator.Send(new ExclusaoLivroCommand(id)));
        }
    }
}
