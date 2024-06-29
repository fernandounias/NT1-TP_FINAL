namespace ParkingLotManagment.Models
{
    public class Ticket
    {
        public Guid Id { get; set; }
        public DateTime Ingreso { get; set; }
        public DateTime Salida { get; set; }
        public string Patente { get; set; }
        public decimal Tarifa { get; set; }

    }
}
