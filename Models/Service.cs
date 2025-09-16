namespace PoryectServiceApi.Models
{
    public class Service
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public string? Equipo { get; set; }
        public string? Diagnostico { get; set; }
        public string? Telefono { get; set; }
        public decimal Valor { get; set; }
        public decimal Seña { get; set; }
        public DateTime FechaIngreso { get; set; }
    }
}
