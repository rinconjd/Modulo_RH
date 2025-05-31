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
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<ClienteService>();
builder.Services.AddScoped<ITransaccionRepository, TransaccionRepository>();
builder.Services.AddScoped<TransaccionService>();
builder.Services.AddScoped<ITransaccionRepository, TransaccionRepository>();
builder.Services.AddScoped<TransaccionService>();

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

// Agrega esto después de builder.Services.AddAuthorization();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Inyección de dependencias
builder.Services.AddScoped<IEmpleadoRepository, EmpleadoRepository>();

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

app.UseCors("AllowAll");

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
            Cedula = "102030405",
            Rol = "Administrador",
            FechaIngreso = DateTime.UtcNow,
            Usuario = usuario
        };

        var usuario2 = new Usuario
        {
            Username = "juanperez",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("jp123"),
            Rol = "Clientes"
        };

        var empleado2 = new Empleado
        {
            Nombre = "Juan Pérez",
            Cedula = "123456789",
            Rol = "Clientes",
            FechaIngreso = DateTime.UtcNow,
            Usuario = usuario2
        };

        var usuario3 = new Usuario
        {
            Username = "diegomez",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("dg123"),
            Rol = "Inventario"
        };

        var empleado3 = new Empleado
        {
            Nombre = "Diego Gómez",
            Cedula = "100101010",
            Rol = "Inventario",
            FechaIngreso = DateTime.UtcNow,
            Usuario = usuario3
        };
        var usuarioCliente1 = new Usuario
        {
            Username = "maria.ruiz@email.com",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("mr123"),
            Rol = "Ordenes"
        };

        var cliente1 = new Cliente
        {
            Nombre = "María",
            Apellido = "Ruiz",
            Cedula = 123456789,
            Correo = "maria.ruiz@email.com",
            Telefono = "3001234567",
            Usuario = usuarioCliente1
        };

        var usuarioCliente2 = new Usuario
        {
            Username = "carlos.lopez@email.com",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("cl123"),
            Rol = "Ordenes"
        };

        var cliente2 = new Cliente
        {
            Nombre = "Carlos",
            Apellido = "López",
            Cedula = 987654321,
            Correo = "carlos.lopez@email.com",
            Telefono = "3012345678",
            Usuario = usuarioCliente2
        };

        var usuarioCliente3 = new Usuario
        {
            Username = "laura.gonzalez@email.com",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("lg123"),
            Rol = "Ordenes"
        };

        var cliente3 = new Cliente
        {
            Nombre = "Laura",
            Apellido = "González",
            Cedula = 456789123,
            Correo = "laura.gonzalez@email.com",
            Telefono = "3023456789",
            Usuario = usuarioCliente3
        };

        // Agrega estos a la base de datos
        context.Usuarios.Add(usuarioCliente1);
        context.Clientes.Add(cliente1);
        context.Usuarios.Add(usuarioCliente2);
        context.Clientes.Add(cliente2);
        context.Usuarios.Add(usuarioCliente3);
        context.Clientes.Add(cliente3);

        context.Usuarios.Add(usuario);
        context.Empleados.Add(empleado);
        context.Usuarios.Add(usuario2);
        context.Empleados.Add(empleado2);
        context.Usuarios.Add(usuario3);
        context.Empleados.Add(empleado3);

        // Busca los clientes para obtener sus Ids o Cédulas
        var clienteMaria = context.Clientes.FirstOrDefault(c => c.Correo == "maria.ruiz@email.com");
        var clienteCarlos = context.Clientes.FirstOrDefault(c => c.Correo == "carlos.lopez@email.com");
        var clienteLaura = context.Clientes.FirstOrDefault(c => c.Correo == "laura.gonzalez@email.com");

        // Crea transacciones de ejemplo
        var transaccion1 = new Transaccion
        {
            Id = Guid.NewGuid(),
            CompraId = Guid.NewGuid(),
            ClienteCedula = clienteMaria?.Cedula ?? 0,
            Monto = 150000,
            Fecha = DateTime.UtcNow.AddDays(-2)
        };

        var transaccion2 = new Transaccion
        {
            Id = Guid.NewGuid(),
            CompraId = Guid.NewGuid(),
            ClienteCedula = clienteCarlos?.Cedula ?? 0,
            Monto = 250000,
            Fecha = DateTime.UtcNow.AddDays(-1)
        };

        var transaccion3 = new Transaccion
        {
            Id = Guid.NewGuid(),
            CompraId = Guid.NewGuid(),
            ClienteCedula = clienteLaura?.Cedula ?? 0,
            Monto = 350000,
            Fecha = DateTime.UtcNow
        };

        context.Transacciones.AddRange(transaccion1, transaccion2, transaccion3);
        context.SaveChanges();
    }
}


app.Run();
