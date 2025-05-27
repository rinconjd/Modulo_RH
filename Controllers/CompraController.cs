using Microsoft.AspNetCore.Mvc;
using RecursosHumanosAPI.Models;
using RecursosHumanosAPI.Services;
using System;

namespace RecursosHumanosAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ComprasController : ControllerBase
    {
        private readonly CompraService _servicio;

        public ComprasController(CompraService servicio)
        {
            _servicio = servicio;
        }

        [HttpGet]
        public IActionResult GetAll() => Ok(_servicio.ObtenerTodos());

        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var compra = _servicio.ObtenerPorId(id);
            return compra is null ? NotFound() : Ok(compra);
        }

        [HttpPost]
        public IActionResult Create(Compra compra)
        {
            _servicio.Crear(compra);
            return CreatedAtAction(nameof(Get), new { id = compra.Id }, compra);
        }

        [HttpPut("{id}")]
        public IActionResult Update(Guid id, [FromBody] Compra compra)
        {
            compra.Id = id;
            var resultado = _servicio.ActualizarCompra(compra);

            if (resultado == "Compra no encontrada")
                return NotFound(new { mensaje = resultado });

            return Ok(compra);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            _servicio.Eliminar(id);
            return NoContent();
        }
    }
}