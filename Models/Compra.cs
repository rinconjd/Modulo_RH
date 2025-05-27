using System;

namespace RecursosHumanosAPI.Models
{
    public class Compra
    {
        public Guid Id { get; set; }
        public int ClienteCedula { get; set; }
        public DateTime Fecha { get; set; }
    }
}