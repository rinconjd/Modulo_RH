using System;

namespace RecursosHumanosAPI.Models
{
    public class Transaccion
    {
        public Guid Id { get; set; }
        public Guid CompraId { get; set; }
        public int ClienteCedula { get; set; }
        public double Monto { get; set; }
        public DateTime Fecha { get; set; }
    }
}