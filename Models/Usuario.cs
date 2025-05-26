using System.Text.Json.Serialization;
using RecursosHumanosAPI.Models;

public class Usuario
{
    public int Id { get; set; }
    public required string Username { get; set; }
    public required string PasswordHash { get; set; } // Idealmente encriptado
    public required string Rol { get; set; }

    [JsonIgnore]
    public Empleado? Empleado { get; set; }

}
