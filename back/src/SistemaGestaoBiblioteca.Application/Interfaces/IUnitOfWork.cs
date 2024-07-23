using Microsoft.EntityFrameworkCore.Storage;

namespace SistemaGestaoBiblioteca.Application.Interfaces
{
    public interface IUnitOfWork
    {
        Task CommitAsync();
        IDbContextTransaction BeginTransaction();
        Task<IDbContextTransaction> BeginTransactionAsync();
        Task CommitTransactionAsync(IDbContextTransaction transaction);
        IGeneroRepository GeneroRepository { get; }
        ILivroRepository LivroRepository { get; }
        IAutorRepository AutorRepository { get; }
    }
}
