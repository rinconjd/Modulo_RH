using Microsoft.AspNetCore.Mvc;
using RecursosHumanosAPI.Models;
using RecursosHumanosAPI.Services;
using System;

namespace RecursosHumanosAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransaccionesController : ControllerBase
    {
        private readonly TransaccionService _servicio;

        public TransaccionesController(TransaccionService servicio)
        {
            _servicio = servicio;
        }

        [HttpGet]
        public IActionResult GetAll() => Ok(_servicio.ObtenerTodos());

        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var transaccion = _servicio.ObtenerPorId(id);
            return transaccion is null ? NotFound() : Ok(transaccion);
        }

        [HttpPost]
        public IActionResult Create(Transaccion transaccion)
        {
            _servicio.Crear(transaccion);
            return CreatedAtAction(nameof(Get), new { id = transaccion.Id }, transaccion);
        }

        [HttpPut("{id}")]
        public IActionResult Update(Guid id, [FromBody] Transaccion transaccion)
        {
            transaccion.Id = id;
            var resultado = _servicio.ActualizarTransaccion(transaccion);

            if (resultado == "Transacción no encontrada")
                return NotFound(resultado);

            return Ok(resultado);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            _servicio.Eliminar(id);
            return NoContent();
        }
    }
}