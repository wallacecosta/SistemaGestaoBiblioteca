namespace SistemaGestaoBiblioteca.Domain.Entidades
{
    public class Livro : EntidadeBase
    {
        public string Nome { get; private set; }
        public Guid AutorId { get; private set; }
        public Guid GeneroId { get; private set; }
        public virtual Genero Genero { get; }
        public virtual Autor Autor { get; }

        public Livro(string nome, Genero genero, Autor autor)
        {
            Nome = string.IsNullOrWhiteSpace(nome) ? throw new ArgumentException("Nome precisa ser válido") : nome;
            Genero = genero ?? throw new ArgumentNullException(nameof(genero));
            Autor = autor ?? throw new ArgumentNullException(nameof(autor));
            AutorId = autor.Id;
            GeneroId = genero.Id;
        }

        public void AlterarNome(string nome)
            => Nome = string.IsNullOrWhiteSpace(nome) ? throw new ArgumentException("Nome precisa ser válido") : nome;

        public void AlterarAutor(Autor autor)
            => AutorId = autor.Id;

        public void AlterarGenero(Genero genero)
            => GeneroId = genero.Id;
    }
}