namespace SistemaGestaoBiblioteca.Domain.Entidades
{
    public abstract class EntidadeBase
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public DateTime DataCadastro { get; private set; } = DateTime.Now;
    }
}
