using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

public class ArchivoSchedulerService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;

    private readonly ArchivoConciliacionService _archivoConciliacionService;

    public ArchivoSchedulerService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var now = DateTime.Now;
            var targetTime = DateTime.Today.AddHours(23); // 11:00 p.m.

            if (now > targetTime)
                targetTime = targetTime.AddDays(1);

            var delay = targetTime - now;
            await Task.Delay(delay, stoppingToken);

            // Generar el archivo
            using (var scope = _serviceProvider.CreateScope())
            {
                var archivoService = scope.ServiceProvider.GetRequiredService<ArchivoConciliacionService>();
                _archivoConciliacionService.GenerarContenidoTransacciones(); // Método que genera el archivo
            }
        }
    }
}