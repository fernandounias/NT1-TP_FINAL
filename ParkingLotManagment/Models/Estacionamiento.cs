using System.ComponentModel.DataAnnotations;

namespace ParkingLotManagment.Models
{
    public class Estacionamiento
    {
        //[Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        [Display(Name = "Nombre")]
        [RegularExpression(@"[a-zA-Z0-9áéíóú_]*", ErrorMessage = "Este campo solo acepta caracteres alfanuméricos y _")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        [Display(Name = "n° Máximo de Plazas")]
        [RegularExpression(@"[0-9]*", ErrorMessage = "Este campo solo acepta caracteres numéricos")]
        public int MaxPlazas { get; set; }

        [Display(Name = "Lista de Vehículos")]
        [ScaffoldColumn(false)]
        public List<Vehiculo> Plazas { get; set; }
    }
}
