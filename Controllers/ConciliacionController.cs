using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecursosHumanosAPI.Models;
using RecursosHumanosAPI.Services;

[ApiController]
[Route("api/[controller]")]
public class ConciliacionController : ControllerBase
{
    private readonly ArchivoConciliacionService _archivoService;

    public ConciliacionController(ArchivoConciliacionService archivoService)
    {
        _archivoService = archivoService;
    }

    // [HttpGet("comparar/{cedula}")]
    // public async Task<IActionResult> CompararTransacciones(string cedula)
    // {
    //     if (!int.TryParse(cedula, out int cedulaInt))
    //         return BadRequest("Cédula no válida");

    //     var locales = _archivoService.LeerDesdeArchivo()
    //         .Where(t => t.ClienteCedula == cedulaInt)
    //         .ToList();

    //     var banco = await _bancoService.ObtenerTransaccionesBanco(cedula);
    //     if (banco == null) return NotFound("No se pudieron obtener transacciones del banco");

    //     var diferencias = locales.Where(l => !banco.Any(b =>
    //         b.Id == l.Id &&
    //         b.Monto == l.Monto &&
    //         b.Fecha.Date == l.Fecha.Date
    //     )).ToList();

    //     return Ok(new
    //     {
    //         TotalLocales = locales.Count,
    //         TotalBanco = banco.Count,
    //         Diferencias = diferencias
    //     });
    // }

    // [HttpGet("archivo")]
    // public IActionResult ObtenerArchivoConciliacion()
    // {
    //     var ruta = "Conciliaciones/transacciones.txt";
    //     if (!System.IO.File.Exists(ruta))
    //         return NotFound("Archivo de conciliación no encontrado");

    //     var contenido = System.IO.File.ReadAllText(ruta);
    //     return Ok(new { contenido });
    // }


    [HttpPost("procesar")]
    public async Task<IActionResult> ProcesarArchivoConciliacion()
    {
        // Genera el contenido automáticamente usando el servicio
        var contenido = _archivoService.GenerarContenidoTransacciones();

        if (string.IsNullOrWhiteSpace(contenido))
        {
            Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] No hay transacciones para procesar.");
            return BadRequest("No hay transacciones para procesar.");
        }

        // Log para verificar el contenido generado
        Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] Contenido generado para el archivo:");
        Console.WriteLine(contenido);

        using var httpClient = new HttpClient();
        var url = "http://10.43.103.210:8081/api/conciliacion/procesar";

        var httpRequest = new HttpRequestMessage(HttpMethod.Post, url)
        {
            Content = new StringContent(contenido, System.Text.Encoding.UTF8, "text/plain")
        };

        try
        {
            var response = await httpClient.SendAsync(httpRequest);
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Error al comunicarse con el servicio externo. Código de estado: {response.StatusCode}");
                return StatusCode((int)response.StatusCode, "Error al comunicarse con el servicio externo");
            }

            var respuestaJson = await response.Content.ReadAsStringAsync();
            return Content(respuestaJson, "application/json");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Excepción al intentar comunicarse con el servicio externo: {ex.Message}");
            return StatusCode(500, "Error interno al intentar comunicarse con el servicio externo");
        }
    }

    [HttpGet("archivo")]
    public IActionResult ObtenerArchivoConciliacion()
    {
        var contenido = _archivoService.GenerarContenidoTransacciones();

        if (string.IsNullOrWhiteSpace(contenido))
        {
            Console.WriteLine("No hay transacciones para procesar.");
            return NotFound("No hay transacciones para procesar.");
        }

        Console.WriteLine("Contenido generado para el archivo:");
        Console.WriteLine(contenido);

        return Ok(new { contenido });
    }
}