namespace SistemaGestaoBiblioteca.Domain.Entidades
{
    public abstract class EntidadeBase
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public DateTime DataCadastro { get; private set; } = DateTime.Now;
        public DateTime? DataExclusao { get; private set; }

        public void Excluir()
            => DataExclusao = DateTime.Now;
    }
}
