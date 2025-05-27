using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecursosHumanosAPI.Models;
using RecursosHumanosAPI.Services;

namespace RecursosHumanosAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientesController : ControllerBase
    {
        private readonly ClienteService _servicio;

        public ClientesController(ClienteService servicio)
        {
            _servicio = servicio;
        }

        [HttpGet]
        public IActionResult GetAll() => Ok(_servicio.ObtenerTodos());

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var cliente = _servicio.ObtenerPorId(id);
            return cliente is null ? NotFound() : Ok(cliente);
        }

        [HttpPost]
        public IActionResult Create(Cliente cliente)
        {
            _servicio.Crear(cliente);
            return CreatedAtAction(nameof(Get), new { id = cliente.Id }, cliente);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Update(int id, [FromBody] Cliente cliente)
        {
            cliente.Id = id;
            var resultado = _servicio.ActualizarCliente(cliente);

            if (resultado == "Cliente no encontrado")
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
        public IActionResult FiltrarClientes(
            string? nombre,
            string? apellido,
            string? correo,
            string? telefono)
        {
            var lista = _servicio.ObtenerTodos();

            if (!string.IsNullOrEmpty(nombre))
                lista = lista.Where(c => c.Nombre.Contains(nombre)).ToList();

            if (!string.IsNullOrEmpty(apellido))
                lista = lista.Where(c => c.Apellido.Contains(apellido)).ToList();

            if (!string.IsNullOrEmpty(correo))
                lista = lista.Where(c => c.Correo.Contains(correo)).ToList();

            if (!string.IsNullOrEmpty(telefono))
                lista = lista.Where(c => c.Telefono.Contains(telefono)).ToList();

            return Ok(lista);
        }
    }
}