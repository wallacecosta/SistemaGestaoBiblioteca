using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaGestaoBiblioteca.Domain.Entidades;

namespace SistemaGestaoBiblioteca.Infrastructure.Configuration
{
    internal class AutorConfiguration : IEntityTypeConfiguration<Autor>
    {
        public void Configure(EntityTypeBuilder<Autor> builder)
        {
            builder.ToTable("Autores");

            builder.HasKey(k => k.Id);

            builder.Property(p => p.Nome)
                .IsRequired();

            builder.Property(p => p.Sobrenome)
                .IsRequired();

            builder.HasMany(l => l.Livros)
                .WithOne(a => a.Autor)
                .HasForeignKey(fk => fk.AutorId)
                .HasConstraintName("FK_Livros_Autor");
        }
    }
}