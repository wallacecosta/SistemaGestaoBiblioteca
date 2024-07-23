using SistemaGestaoBiblioteca.Domain.Entidades;

namespace SistemaGestaoBiblioteca.Application.Model
{
    public record LivroModel(Guid Id, string Nome, AutorModel Autor, GeneroModel Genero)
    {
        public static LivroModel MapFrom(Livro livro)
        {
            var autorModel = AutorModel.MapFrom(livro.Autor);
            var generoModel = GeneroModel.MapFrom(livro.Genero);

            return new LivroModel(livro.Id, livro.Nome, autorModel, generoModel);
        }
    }
}