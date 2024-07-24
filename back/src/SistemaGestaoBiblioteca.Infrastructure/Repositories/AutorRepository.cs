using SistemaGestaoBiblioteca.Application.Interfaces;
using SistemaGestaoBiblioteca.Domain.Entidades;
using SistemaGestaoBiblioteca.Infrastructure.Context;

namespace SistemaGestaoBiblioteca.Infrastructure.Repositories
{
    public class AutorRepository(AppDbContext context) : Repository<Autor>(context), IAutorRepository
    {
    }
}
