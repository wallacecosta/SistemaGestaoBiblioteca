using SistemaGestaoBiblioteca.Application.Interfaces;
using SistemaGestaoBiblioteca.Domain.Entidades;
using SistemaGestaoBiblioteca.Infrastructure.Context;

namespace SistemaGestaoBiblioteca.Infrastructure.Repositories
{
    public class GeneroRepository(AppDbContext context) : Repository<Genero>(context), IGeneroRepository
    {
    }
}
