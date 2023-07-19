using ApiAutores.Entidades;
using Microsoft.EntityFrameworkCore;

namespace ApiAutores
{
    public class AplicationDBContext : DbContext
    {
        public AplicationDBContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<AutoreLibro>()
                .HasKey(al => new { al.AutorId, al.libroId });
        }


        public DbSet<Autores> Autores { get; set; }

        public DbSet<Libro> Libros { get; set; }

        public DbSet<Comentario> Comentario { get; set; }

        public DbSet<AutoreLibro> AutoreLibro { get; set; }


    }
}
