using MediatR;

namespace SistemaGestaoBiblioteca.Application.Commands.Livros.Exclusao
{
    public record ExclusaoLivroCommand(Guid LivroId) : IRequest<ExclusaoLivroResponse>;
}
