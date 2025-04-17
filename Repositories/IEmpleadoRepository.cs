using RecursosHumanosAPI.Models;

namespace RecursosHumanosAPI.Repositories
{
    public interface IEmpleadoRepository
    {
        List<Empleado> GetAll();
        Empleado? GetById(int id);
        void Create(Empleado empleado);
        void Update(Empleado empleado);
        void Delete(int id);
    }
}
