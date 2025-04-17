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
        public IActionResult Update(int id, Empleado emp)
        {
            emp.Id = id;
            _servicio.Actualizar(emp);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _servicio.Eliminar(id);
            return NoContent();
        }
    }
}
