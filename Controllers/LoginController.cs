using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class LoginController : ControllerBase
{
    private readonly AuthService _authService;
    private readonly JwtService _jwtService;

    public LoginController(AuthService authService, JwtService jwtService)
    {
        _authService = authService;
        _jwtService = jwtService;
    }

    [HttpPost]
    public IActionResult Login([FromBody] LoginRequest request)
    {
        var user = _authService.Authenticate(request.Username, request.Password);
        if (user == null)
            return Unauthorized("Credenciales inválidas");

        // Verifica que el rol sea permitido
        var rolesPermitidos = new[] { "Empleado", "Inventario", "Admin" };
        if (!rolesPermitidos.Contains(user.Rol))
            return Forbid("No tiene permisos para ingresar a esta aplicación");

        var token = _jwtService.GenerateToken(user);
        return Ok(new { token });
    }

    [HttpPost("Inventario")]
    public IActionResult LoginInventario([FromBody] LoginRequest request)
    {
        var user = _authService.Authenticate(request.Username, request.Password);
        if (user == null)
            return Unauthorized("Credenciales inválidas");

        if (user.Rol != "Inventario")
            return Forbid("No tiene permisos para ingresar a esta aplicación");

        var token = _jwtService.GenerateToken(user);
        return Ok(new { token });
    }

    [HttpPost("Cliente")]
    public IActionResult LoginCliente([FromBody] LoginRequest request)
    {
        Console.WriteLine($"Intento de inicio de sesión (Cliente): Usuario = {request.Username}, Contraseña = {request.Password}");

        var user = _authService.Authenticate(request.Username, request.Password);
        if (user == null)
        {
            Console.WriteLine("Credenciales inválidas");
            return Unauthorized("Credenciales inválidas");
        }

        if (user.Rol != "Cliente")
        {
            Console.WriteLine($"Acceso denegado: Usuario = {request.Username}, Rol = {user.Rol}");
            return Forbid("No tiene permisos para ingresar a esta aplicación");
        }

        var token = _jwtService.GenerateToken(user);
        Console.WriteLine($"Inicio de sesión exitoso (Cliente): Usuario = {request.Username}, Rol = {user.Rol}");
        return Ok(new { token });
    }

}
