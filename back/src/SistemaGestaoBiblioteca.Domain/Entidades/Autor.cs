namespace SistemaGestaoBiblioteca.Domain.Entidades
{
    public class Autor : EntidadeBase
    {
        public string Nome { get; }
        public string Sobrenome { get; }
        public ICollection<Livro>? Livros { get; }

        public Autor(string nome, string sobrenome)
        {
            Nome = string.IsNullOrWhiteSpace(nome) ? throw new ArgumentException("Nome é obrigatório!") : nome;
            Sobrenome = string.IsNullOrWhiteSpace(sobrenome) ? throw new ArgumentException("Sobrenome é obrigatório!") : sobrenome;
        }
    }
}
