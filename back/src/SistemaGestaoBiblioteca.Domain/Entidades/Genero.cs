namespace SistemaGestaoBiblioteca.Domain.Entidades
{
    public class Genero : EntidadeBase
    {
        public string Nome { get; }
        public ICollection<Livro>? Livros { get; }

        public Genero(string nome)
        {
            Nome = string.IsNullOrWhiteSpace(nome) ? throw new ArgumentException("Nome do Gênero deve ser informado") : nome;
        }
    }
}