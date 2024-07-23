using MediatR;

namespace SistemaGestaoBiblioteca.Application.Commands.Autores.CadastroDeAutor
{
    public record CadastroAutorCommand(string Nome, string Sobrenome) : IRequest<CadastroAutorResponse>;
}
