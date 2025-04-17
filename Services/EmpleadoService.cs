using RecursosHumanosAPI.Models;
using RecursosHumanosAPI.Repositories;

namespace RecursosHumanosAPI.Services
{
    public class EmpleadoService
    {
        private readonly IEmpleadoRepository _repo;

        public EmpleadoService(IEmpleadoRepository repo)
        {
            _repo = repo;
        }

        public List<Empleado> ObtenerTodos() => _repo.GetAll();

        public Empleado? ObtenerPorId(int id) => _repo.GetById(id);

        public void Crear(Empleado empleado) => _repo.Create(empleado);

        public void Actualizar(Empleado empleado) => _repo.Update(empleado);

        public void Eliminar(int id) => _repo.Delete(id);
    }
}
