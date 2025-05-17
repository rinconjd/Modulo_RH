using RecursosHumanosAPI.Models;
using RecursosHumanosAPI.Repositories;

namespace RecursosHumanosAPI.Services
{
    public class EmpleadoService
    {
        private readonly IEmpleadoRepository _repo;

        private readonly AuthService _authService;

        public EmpleadoService(IEmpleadoRepository repo, AuthService authService)
        {
            _repo = repo;
            _authService = authService;
        }

        public List<Empleado> ObtenerTodos() => _repo.GetAll();

        public Empleado? ObtenerPorId(int id) => _repo.GetById(id);

        public void Crear(Empleado empleado) => _repo.Create(empleado);

        public void Actualizar(Empleado empleado) => _repo.Update(empleado);

        public string ActualizarEmpleado(Empleado empleado)
        {
            var empExistente = _repo.GetById(empleado.Id);
            if (empExistente == null)
                return "Empleado no encontrado";

            _repo.Update(empleado);

            return "Empleado actualizado correctamente";
        }


        public void Eliminar(int id) => _repo.Delete(id);
    }
}
