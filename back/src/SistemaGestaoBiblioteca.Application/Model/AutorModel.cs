using SistemaGestaoBiblioteca.Domain.Entidades;

namespace SistemaGestaoBiblioteca.Application.Model
{
    public record AutorModel(Guid Id, string Nome, string Sobrenome, List<LivroModel> Livros)
    {
        public static AutorModel MapFrom(Autor autor)
            => new(autor.Id, autor.Nome, autor.Sobrenome, []);
    }
}