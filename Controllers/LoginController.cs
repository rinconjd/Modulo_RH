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
        var user = _authService.Authenticate(request.Username, request.Password);
        if (user == null)
            return Unauthorized("Credenciales inválidas");

        if (user.Rol != "Ordenes")
            return Forbid("No tiene permisos para ingresar a esta aplicación");

        var token = _jwtService.GenerateToken(user);
        return Ok(new { token });
    }

}
