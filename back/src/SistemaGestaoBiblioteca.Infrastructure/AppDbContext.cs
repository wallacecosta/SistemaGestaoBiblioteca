using Microsoft.EntityFrameworkCore;
using SistemaGestaoBiblioteca.Domain.Entidades;
using System.Data;

namespace SistemaGestaoBiblioteca.Infrastructure
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public IDbConnection Connection => Database.GetDbConnection();

        public DbSet<Livro> Livros { get; set; }
        public DbSet<Genero> Generos { get; set; }
        public DbSet<Autor> Autores { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("dbo");

            modelBuilder.ApplyConfiguration(new LivroConfiguration());
            modelBuilder.ApplyConfiguration(new GeneroConfiguration());
            modelBuilder.ApplyConfiguration(new AutorConfiguration());
        }
    }
}
