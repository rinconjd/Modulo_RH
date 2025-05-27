using RecursosHumanosAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace RecursosHumanosAPI.Repositories
{
    public class EmpleadoRepository : IEmpleadoRepository
    {
        private readonly AppDbContext _context;

        public EmpleadoRepository(AppDbContext context)
        {
            _context = context;
        }

        public List<Empleado> GetAll()
        {
            return _context.Empleados
                .Include(e => e.Usuario) // si la entidad tiene relación
                .ToList();
        }

        public Empleado? GetById(int id)
        {
            return _context.Empleados
                .Include(e => e.Usuario)
                .FirstOrDefault(e => e.Id == id);
        }

        public void Create(Empleado empleado)
        {
            _context.Empleados.Add(empleado);
            _context.SaveChanges();
        }

        public void Update(Empleado empleado)
        {
            var existente = _context.Empleados
                .Include(e => e.Usuario)
                .FirstOrDefault(e => e.Id == empleado.Id);

            if (existente == null)
                return;

            // Actualiza solo las propiedades necesarias
            existente.Nombre = empleado.Nombre;
            existente.Cedula = empleado.Cedula;
            existente.Rol = empleado.Rol;
            existente.FechaIngreso = empleado.FechaIngreso;
            existente.UsuarioId = empleado.UsuarioId;

            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            // En tu servicio o repositorio antes de borrar el empleado:
            var empleado = _context.Empleados.Include(e => e.Usuario).FirstOrDefault(e => e.Id == id);
            if (empleado != null)
            {
                if (empleado.Usuario != null)
                    _context.Usuarios.Remove(empleado.Usuario);

                _context.Empleados.Remove(empleado);
                _context.SaveChanges();
            }
        }
    }
}
