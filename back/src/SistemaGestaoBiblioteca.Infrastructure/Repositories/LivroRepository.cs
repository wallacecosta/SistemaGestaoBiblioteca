using SistemaGestaoBiblioteca.Application.Interfaces;
using SistemaGestaoBiblioteca.Domain.Entidades;
using SistemaGestaoBiblioteca.Infrastructure.Context;

namespace SistemaGestaoBiblioteca.Infrastructure.Repositories
{
    public class LivroRepository(AppDbContext context) : Repository<Livro>(context), ILivroRepository
    {
    }
}
