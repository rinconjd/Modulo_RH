using RecursosHumanosAPI.Models;
using RecursosHumanosAPI.Services;

public class ArchivoConciliacionService
{
    private readonly string ruta = "Conciliaciones/transacciones.txt";

    private readonly TransaccionService _transaccionService;

    public ArchivoConciliacionService(TransaccionService transaccionService)
    {
        _transaccionService = transaccionService ?? throw new ArgumentNullException(nameof(transaccionService));
    }

    public string GenerarContenidoTransacciones()
    {
        var transacciones = _transaccionService.ObtenerTodos();
        var lineas = transacciones
            .Select(t => $"{t.ClienteCedula},{t.Fecha.ToUniversalTime():yyyy-MM-ddTHH:mm:ss.ffffffZ},{t.Monto}")
            .ToList();
        return string.Join('\n', lineas);
    }
}