using System.ComponentModel.DataAnnotations;
using System.Data;

namespace ParkingLotManagment.Models
{
    public class Ticket
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public DateTime Ingreso { get; set; }
        public DateTime Salida { get; set; }
        [Required]
        public string Patente { get; set; }
        public decimal Tarifa { get; set; }


        
}
