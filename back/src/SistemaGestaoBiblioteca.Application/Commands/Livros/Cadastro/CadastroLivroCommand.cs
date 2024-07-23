using MediatR;
using SistemaGestaoBiblioteca.Application.Model;

namespace SistemaGestaoBiblioteca.Application.Commands.Livros.Cadastro
{
    public record CadastroLivroCommand(string Nome, AutorModel Autor, GeneroModel Genero) : IRequest<CadastroLivroResponse>;
}
