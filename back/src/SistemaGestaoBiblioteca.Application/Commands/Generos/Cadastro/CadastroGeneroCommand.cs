using MediatR;

namespace SistemaGestaoBiblioteca.Application.Commands.Generos.Cadastro
{
    public record CadastroGeneroCommand(string Nome) : IRequest<CadastroGeneroResponse>;
}
