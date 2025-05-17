using Microsoft.EntityFrameworkCore;
using RecursosHumanosAPI.Models;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Empleado> Empleados { get; set; }
    public DbSet<Usuario> Usuarios { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Empleado>()
            .HasOne(e => e.Usuario)
            .WithOne(u => u.Empleado)
            .HasForeignKey<Empleado>(e => e.UsuarioId)
            .IsRequired(false);
    }

}
