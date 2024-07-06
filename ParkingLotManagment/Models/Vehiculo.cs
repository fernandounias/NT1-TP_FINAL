using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace ParkingLotManagment.Models
{
    public class Vehiculo
    {
        [Key]
        public string Patente { get; set; }
        
        [Required]
        public string Marca { get; set; }
        
        [Required]
        public string Modelo { get; set; }
        
        public List<Ticket> Tickets { get; set; }

        [Required]
        public TipoVehiculo TipoDeVehiculo{ get; set; }

    }
}
