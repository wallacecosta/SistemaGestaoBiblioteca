using MediatR;

namespace SistemaGestaoBiblioteca.Application.CadastroDeLivro
{
    public record CadastroLivroCommand(CadastrarLivroRequest Request) : IRequest<CadastroLivroResponse>;
}
