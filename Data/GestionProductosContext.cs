using GestionProductosAPI.Models;
using Microsoft.EntityFrameworkCore;


namespace GestionProductosAPI.Data
    //es el puente entre el código y la base de datos
{
    public class GestionProductosContext : DbContext
    {
        public GestionProductosContext(DbContextOptions<GestionProductosContext> options)
            : base(options)
        {

        }

        //Aquí se agregan las tablas (Atributos de la clase Producto

        public DbSet<Producto> Productos { get; set; }

        //Aquí configuramos la precisión de Precio 

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Producto>()
                .Property(p => p.Precio)
                .HasPrecision(18, 2); //máximo 18 dígitos, 2 decimales 
        }
    }
}


/*
 La clase GestionProductosContext representa la base de datos
y con DbSet<Producto> EF Core ya sabe crear la tabla correspondiente
 */