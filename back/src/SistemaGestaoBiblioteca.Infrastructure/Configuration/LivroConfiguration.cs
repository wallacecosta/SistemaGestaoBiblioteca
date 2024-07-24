using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaGestaoBiblioteca.Domain.Entidades;

namespace SistemaGestaoBiblioteca.Infrastructure.Configuration
{
    internal class LivroConfiguration : IEntityTypeConfiguration<Livro>
    {
        public void Configure(EntityTypeBuilder<Livro> builder)
        {
            builder.ToTable("Livros");

            builder.HasKey(k => k.Id);

            builder.Property(p => p.Nome)
                .IsRequired();

            builder.Property(p => p.AutorId)
                .IsRequired();

            builder.Property(p => p.GeneroId)
                .IsRequired();
        }
    }
}