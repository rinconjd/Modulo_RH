namespace RecursosHumanosAPI.Models
{
    public class Empleado
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Documento { get; set; }
        public string Cargo { get; set; }
        public string Area { get; set; }
        public DateTime FechaIngreso { get; set; }
    }
}
