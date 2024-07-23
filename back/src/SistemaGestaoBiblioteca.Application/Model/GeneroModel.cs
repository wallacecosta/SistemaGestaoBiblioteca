using SistemaGestaoBiblioteca.Domain.Entidades;

namespace SistemaGestaoBiblioteca.Application.Model
{
    public record GeneroModel(Guid Id, string Nome, List<LivroModel> Livros)
    {
        public static GeneroModel MapFrom(Genero genero)
            => new(genero.Id, genero.Nome, []);
    }
}