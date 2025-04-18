using Microsoft.EntityFrameworkCore;
using RecursosHumanosAPI.Repositories;
using RecursosHumanosAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Agrega controladores (API REST)
builder.Services.AddControllers();

// Agrega Swagger (documentación y pruebas)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Inyección de dependencias
builder.Services.AddSingleton<IEmpleadoRepository, EmpleadoRepository>();
builder.Services.AddSingleton<EmpleadoService>();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


var app = builder.Build();

// Configura Swagger solo en entorno de desarrollo (puedes quitar el if si lo quieres siempre)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Usa controladores definidos en la carpeta Controllers/
app.MapControllers();

app.Run();
