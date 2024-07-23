using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaGestaoBiblioteca.Domain.Entidades;

namespace SistemaGestaoBiblioteca.Infrastructure
{
    internal class GeneroConfiguration : IEntityTypeConfiguration<Genero>
    {
        public void Configure(EntityTypeBuilder<Genero> builder)
        {
            throw new NotImplementedException();
        }
    }
}