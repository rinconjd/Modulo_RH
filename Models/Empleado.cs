using System.Text.Json.Serialization;

namespace RecursosHumanosAPI.Models
{
    public class Empleado
    {
        public int Id { get; set; }
        public required string Nombre { get; set; }
        public string? Cedula { get; set; }
        public string? Rol { get; set; }
        public DateTime FechaIngreso { get; set; }

        public int? UsuarioId { get; set; }

        [JsonIgnore]
        public Usuario? Usuario { get; set; }
    }
}
