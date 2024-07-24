using Microsoft.EntityFrameworkCore.Storage;
using SistemaGestaoBiblioteca.Application.Interfaces;
using SistemaGestaoBiblioteca.Infrastructure.Context;

namespace SistemaGestaoBiblioteca.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public ILivroRepository LivroRepository { get; }
        public IAutorRepository AutorRepository { get; }
        public IGeneroRepository GeneroRepository { get; }

        public UnitOfWork(
            AppDbContext appDbContext,
            ILivroRepository livroRepository,
            IAutorRepository autorRepository,
            IGeneroRepository generoRepository)
        {
            _context = appDbContext;
            LivroRepository = livroRepository;
            AutorRepository = autorRepository;
            GeneroRepository = generoRepository;
        }

        public async Task CommitAsync()
            => await _context.SaveChangesAsync();

        public IDbContextTransaction BeginTransaction()
            => _context.Database.BeginTransaction();

        public async Task<IDbContextTransaction> BeginTransactionAsync()
            => await _context.Database.BeginTransactionAsync();

        public async Task CommitTransactionAsync(IDbContextTransaction transaction)
        {
            var avaibleTransaction = transaction ?? _context.Database.CurrentTransaction;

            if (avaibleTransaction is null)
                throw new ArgumentException("Não há transação aberta para commitar");

            await avaibleTransaction.CommitAsync();
            await avaibleTransaction.DisposeAsync();
        }
    }
}
