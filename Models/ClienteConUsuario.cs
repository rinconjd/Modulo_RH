namespace RecursosHumanosAPI.Models
{
    public class ClienteConUsuarioRequest
    {
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public int Cedula { get; set; }
        public string? Correo { get; set; }
        public string? Telefono { get; set; }
        public string? Password { get; set; } // El username será el correo
    }
}