using RecursosHumanosAPI.Models;

namespace RecursosHumanosAPI.Repositories
{
    public class EmpleadoRepository : IEmpleadoRepository
    {
        private readonly List<Empleado> empleados = new();

        public List<Empleado> GetAll() => empleados;

        public Empleado? GetById(int id) => empleados.FirstOrDefault(e => e.Id == id);

        public void Create(Empleado empleado)
        {
            empleado.Id = empleados.Count > 0 ? empleados.Max(e => e.Id) + 1 : 1;
            empleados.Add(empleado);
        }

        public void Update(Empleado empleado)
        {
            var actual = GetById(empleado.Id);
            if (actual == null) return;

            actual.Nombre = empleado.Nombre;
            actual.Documento = empleado.Documento;
            actual.Cargo = empleado.Cargo;
            actual.Area = empleado.Area;
            actual.FechaIngreso = empleado.FechaIngreso;
        }

        public void Delete(int id)
        {
            var emp = GetById(id);
            if (emp != null) empleados.Remove(emp);
        }
    }
}
