using System.ComponentModel.DataAnnotations;

namespace ParkingLotManagment.Models
{
    public class Estacionamiento
    {
        [Key]
        public Guid Id { get; set; }
        public string Nombre { get; set; }
        public int MaxPlazas { get; set; }

        public List<Vehiculo> Plazas { get; set; }
    }
}
