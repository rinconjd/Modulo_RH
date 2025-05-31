using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace RecursosHumanosAPI.Models
{
    public class Cliente
    {
        
        public int Id { get; set; }
        public int Cedula { get; set; }
        public required string Nombre { get; set; }
        public required string Apellido { get; set; }
        public required string Correo { get; set; }
        public required string Telefono { get; set; }

        public int? UsuarioId { get; set; }

        [JsonIgnore]
        public Usuario? Usuario { get; set; }
    }
}