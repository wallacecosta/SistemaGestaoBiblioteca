using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaGestaoBiblioteca.Domain.Entidades;

namespace SistemaGestaoBiblioteca.Infrastructure.Configuration
{
    internal class GeneroConfiguration : IEntityTypeConfiguration<Genero>
    {
        public void Configure(EntityTypeBuilder<Genero> builder)
        {
            builder.ToTable("Generos");

            builder.HasKey(k => k.Id);

            builder.Property(p => p.Nome)
                .IsRequired();

            builder.HasMany(l => l.Livros)
                .WithOne(a => a.Genero)
                .HasForeignKey(fk => fk.GeneroId)
                .HasConstraintName("FK_Livros_Genero");
        }
    }
}