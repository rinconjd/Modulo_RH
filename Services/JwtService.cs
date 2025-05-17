using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

public class JwtService
{
    private readonly string _secretKey;
    private readonly string _issuer;
    private readonly string _audience;

    public JwtService(IConfiguration config)
    {
        _secretKey = config["JwtSettings:SecretKey"] ?? throw new ArgumentNullException("SecretKey");
        _issuer = config["JwtSettings:Issuer"];
        _audience = config["JwtSettings:Audience"];
    }

    public string GenerateToken(Usuario user)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Role, user.Rol)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            _issuer,
            _audience,
            claims,
            expires: DateTime.Now.AddHours(3),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
