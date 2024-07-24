namespace SistemaGestaoBiblioteca.Application.Interfaces
{
    public interface IAutorRepository<T> where T : class
    {
        Task<T?> GetAsync(Guid id);
        Task<IEnumerable<T>> GetAllAsync();
        Task AddAsync(T entity);
        void Delete(T entity);
        void Update(T entity);
    }
}
