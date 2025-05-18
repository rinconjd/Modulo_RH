using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using RecursosHumanosAPI.Models;
using RecursosHumanosAPI.Repositories;
using RecursosHumanosAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Agrega controladores (API REST)
builder.Services.AddControllers();

// Agrega Swagger (documentación y pruebas)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<JwtService>();
builder.Services.AddScoped<EmpleadoService>();


builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "ModuloRH API", Version = "v1" });

    // ✅ Aquí empieza la configuración para el botón Authorize
    var securityScheme = new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Ingresa: Bearer {tu token JWT}",
        Reference = new OpenApiReference
        {
            Type = ReferenceType.SecurityScheme,
            Id = "Bearer"
        }
    };

    c.AddSecurityDefinition("Bearer", securityScheme);
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { securityScheme, new[] { "Bearer" } }
    });
});



builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
            ValidAudience = builder.Configuration["JwtSettings:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:SecretKey"]))
        };
    });

builder.Services.AddAuthorization();


// Inyección de dependencias
builder.Services.AddSingleton<IEmpleadoRepository, EmpleadoRepository>();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


var app = builder.Build();

// Configura Swagger solo en entorno de desarrollo (puedes quitar el if si lo quieres siempre)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Inicializa la base de datos (Sólo para entorno de desarrollo)
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    // 🔄 Elimina y recrea la base de datos en cada ejecución
    context.Database.EnsureDeleted();    // Elimina la DB
    context.Database.Migrate();          // Aplica todas las migraciones (o usa EnsureCreated para esquemas simples)

    // Opcional: agregar datos de prueba
    //DataSeeder.SeedInicial(context);
}


app.UseHttpsRedirection();

// Usa controladores definidos en la carpeta Controllers/
app.MapControllers();

app.UseAuthentication();
app.UseAuthorization();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    if (!context.Empleados.Any())
    {
        var usuario = new Usuario
        {
            Username = "admin",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin123"),
            Rol = "Admin"
        };

        var empleado = new Empleado
        {
            Nombre = "Administrador General",
            Documento = "123456789",
            Cargo = "Administrador",
            Area = "TI",
            FechaIngreso = DateTime.UtcNow,
            Usuario = usuario
        };

        var usuario2 = new Usuario
        {
            Username = "juanperez",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("jp123"),
            Rol = "Empleado"
        };

        var empleado2 = new Empleado
        {
            Nombre = "Juan Pérez",
            Documento = "123456789",
            Cargo = "Analista",
            Area = "TI",
            FechaIngreso = DateTime.UtcNow,
            Usuario = usuario2
        };

        context.Usuarios.Add(usuario);
        context.Empleados.Add(empleado);
        context.Usuarios.Add(usuario2);
        context.Empleados.Add(empleado2);
        context.SaveChanges();
    }
}


app.Run();
