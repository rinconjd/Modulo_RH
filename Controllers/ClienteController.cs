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

        // ...existing code...
        [HttpPost]
        public IActionResult Create(ClienteConUsuarioRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Correo) ||
                string.IsNullOrWhiteSpace(request.Password) ||
                string.IsNullOrWhiteSpace(request.Nombre) ||
                string.IsNullOrWhiteSpace(request.Cedula.ToString()))
            {
                return BadRequest("Correo, Password, Nombre, and Cedula are required.");
            }

            var usuario = new Usuario
            {
                Username = request.Correo,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
                Rol = "Cliente"
            };

            var cliente = new Cliente
            {
                Nombre = request.Nombre,
                Apellido = request.Apellido ?? string.Empty,
                Cedula = request.Cedula,
                Correo = request.Correo,
                Telefono = request.Telefono ?? string.Empty,
                Usuario = usuario
            };

            _servicio.Crear(cliente);
            return CreatedAtAction(nameof(Get), new { id = cliente.Id }, cliente);
        }
        // ...existing code...

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Cliente cliente)
        {
            cliente.Id = id;

            var resultado = _servicio.ActualizarCliente(cliente);

            if (resultado == "Cliente no encontrado")
                return NotFound(resultado);

            return Ok(new { mensaje = resultado });
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

        [HttpGet("cedula/{cedula}")]
        public IActionResult GetByCedula(int cedula)
        {
            var cliente = _servicio.ObtenerTodos().FirstOrDefault(c => c.Cedula == cedula);
            return cliente is null ? NotFound() : Ok(cliente);
        }

        [HttpPut("cedula/{cedula}")]
        public IActionResult UpdateByCedula(int cedula, [FromBody] Cliente cliente)
        {
            var clienteExistente = _servicio.ObtenerTodos().FirstOrDefault(c => c.Cedula == cedula);
            if (clienteExistente == null)
                return NotFound("Cliente no encontrado");

            // Actualiza los datos del cliente existente
            clienteExistente.Nombre = cliente.Nombre;
            clienteExistente.Apellido = cliente.Apellido;
            clienteExistente.Correo = cliente.Correo;
            clienteExistente.Telefono = cliente.Telefono;

            var resultado = _servicio.ActualizarCliente(clienteExistente);

            if (resultado == "Cliente no encontrado")
                return NotFound(resultado);

            return Ok(new { mensaje = resultado });
        }

        [HttpGet("correo/{correo}")]
        public IActionResult GetByCorreo(string correo)
        {
            var cliente = _servicio.ObtenerTodos().FirstOrDefault(c => c.Correo == correo);
            return cliente is null ? NotFound() : Ok(cliente);
        }
    }
}

