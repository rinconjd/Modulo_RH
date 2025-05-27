using Microsoft.EntityFrameworkCore;
using RecursosHumanosAPI.Models;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Empleado> Empleados { get; set; }
    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Cliente> Clientes { get; set; }

    public DbSet<Transaccion> Transacciones { get; set; }
    //public DbSet<Compra> Compras { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Empleado>()
            .HasOne(e => e.Usuario)
            .WithOne(u => u.Empleado)
            .HasForeignKey<Empleado>(e => e.UsuarioId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Cascade); // <--- Agrega esta línea

        modelBuilder.Entity<Cliente>()
            .HasOne(c => c.Usuario)
            .WithOne()
            .HasForeignKey<Cliente>(c => c.UsuarioId)
            .OnDelete(DeleteBehavior.Cascade);

        // modelBuilder.Entity<Compra>()
        //     .HasOne<Cliente>() // Si tienes la navegación, ponla aquí
        //     .WithMany()
        //     .HasForeignKey(c => c.ClienteCedula)
        //     .OnDelete(DeleteBehavior.Cascade);
    }


}
