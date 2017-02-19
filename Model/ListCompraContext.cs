using Microsoft.EntityFrameworkCore;

namespace ApiTienda.Model{
    public class ListaCompraConext : DbContext
    {
        public ListaCompraConext(DbContextOptions<ListaCompraConext> options)
            : base(options)
        { }

      public DbSet<Producto> Producto { get; set; }
      public DbSet<Tienda> Tienda {get;set;}

      protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Producto>().ToTable("Producto");
            modelBuilder.Entity<Tienda>().ToTable("Tienda");
        }
    }
}