using Microsoft.EntityFrameworkCore;
using RestauranteItaliano.Models;

namespace RestauranteItaliano.Data
{
    public class RestauranteDbContext : DbContext
    {
        public RestauranteDbContext(DbContextOptions<RestauranteDbContext> options)
            : base(options)
        {
        }

        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Platillo> Platillos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuración explícita de la relación 1:N
            modelBuilder.Entity<Platillo>()
                .HasOne(p => p.Categoria)
                .WithMany(c => c.Platillos)
                .HasForeignKey(p => p.CategoriaId)
                .OnDelete(DeleteBehavior.Restrict);

            // Datos semilla iniciales (opcional, útil para probar rápido)
            modelBuilder.Entity<Categoria>().HasData(
                new Categoria { Id = 1, Nombre = "Pastas", Descripcion = "Pastas artesanales de la casa" },
                new Categoria { Id = 2, Nombre = "Pizzas", Descripcion = "Horno tradicional italiano" },
                new Categoria { Id = 3, Nombre = "Postres", Descripcion = "Dulces clásicos italianos" }
            );
        }
    }
}