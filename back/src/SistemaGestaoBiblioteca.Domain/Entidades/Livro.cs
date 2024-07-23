namespace SistemaGestaoBiblioteca.Domain.Entidades
{
    public class Livro : EntidadeBase
    {
        public string Nome { get; }
        public Guid AutorId { get; }
        public Guid GeneroId { get; }
        public virtual Genero Genero { get; }
        public virtual Autor Autor { get; }

        public Livro(string nome, Genero genero, Autor autor)
        {
            Nome = nome ?? throw new ArgumentException("Nome não pode ser nulo");
            Genero = genero ?? throw new ArgumentNullException(nameof(genero));
            Autor = autor ?? throw new ArgumentNullException(nameof(autor));
            AutorId = autor.Id;
            GeneroId = genero.Id;
        }
    }
}