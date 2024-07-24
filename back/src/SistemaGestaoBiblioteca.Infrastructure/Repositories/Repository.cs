using Microsoft.EntityFrameworkCore;
using SistemaGestaoBiblioteca.Application.Interfaces;
using SistemaGestaoBiblioteca.Infrastructure.Context;

namespace SistemaGestaoBiblioteca.Infrastructure.Repositories
{
    public abstract class Repository<T> : IAutorRepository<T> where T : class
    {
        protected readonly AppDbContext _context;
        protected Repository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<T?> GetAsync(Guid id)
        {
            return await _context.Set<T>().FindAsync(id);
        }
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }
        public async Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
        }
        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }
        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }
    }
}
