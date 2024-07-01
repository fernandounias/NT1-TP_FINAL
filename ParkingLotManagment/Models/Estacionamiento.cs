using System.ComponentModel.DataAnnotations;

namespace ParkingLotManagment.Models
{
    public class Estacionamiento
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public int MaxPlazas { get; set; }

        public List<Vehiculo> Plazas { get; set; }
    }
}
