using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecursosHumanosAPI.Models;
using RecursosHumanosAPI.Services;

namespace RecursosHumanosAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmpleadosController : ControllerBase
    {
        private readonly EmpleadoService _servicio;

        public EmpleadosController(EmpleadoService servicio)
        {
            _servicio = servicio;
        }

        [HttpGet]
        public IActionResult GetAll() => Ok(_servicio.ObtenerTodos());

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var emp = _servicio.ObtenerPorId(id);
            return emp is null ? NotFound() : Ok(emp);
        }

        [HttpPost]
        public IActionResult Create(Empleado emp)
        {
            _servicio.Crear(emp);
            return CreatedAtAction(nameof(Get), new { id = emp.Id }, emp);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Update(int id, [FromBody] Empleado emp)
        {
            emp.Id = id;
            var resultado = _servicio.ActualizarEmpleado(emp);

            if (resultado == "Empleado no encontrado")
                return NotFound(resultado);

            return Ok(resultado);
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _servicio.Eliminar(id);
            return NoContent();
        }

        [HttpGet("filtrar")]
        [Authorize(Roles = "Admin")]
        public IActionResult FiltrarEmpleados(
            string? nombre,
            string? cargo,
            string? area,
            string? rolUsuario)
        {
            var lista = _servicio.ObtenerTodos();

            if (!string.IsNullOrEmpty(nombre))
                lista = lista.Where(e => e.Nombre.Contains(nombre)).ToList();

            if (!string.IsNullOrEmpty(cargo))
                lista = lista.Where(e => e.Cargo == cargo).ToList();

            if (!string.IsNullOrEmpty(area))
                lista = lista.Where(e => e.Area == area).ToList();

            if (!string.IsNullOrEmpty(rolUsuario))
                lista = lista.Where(e => e.Usuario != null && e.Usuario.Rol == rolUsuario).ToList();

            return Ok(lista);
        }


    }
}
