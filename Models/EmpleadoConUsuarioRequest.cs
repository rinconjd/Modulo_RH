namespace RecursosHumanosAPI.Models
{
    public class EmpleadoConUsuarioRequest
    {
        public string? Nombre { get; set; }
        public string? Cedula { get; set; }
        public string? Rol { get; set; }
        public string? Area { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
    }
}