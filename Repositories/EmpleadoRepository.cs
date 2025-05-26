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
            _context.Empleados.Update(empleado);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var emp = _context.Empleados.Find(id);
            if (emp != null)
            {
                _context.Empleados.Remove(emp);
                _context.SaveChanges();
            }
        }
    }
}
