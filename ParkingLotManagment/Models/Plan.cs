using System.ComponentModel.DataAnnotations;

namespace ParkingLotManagment.Models
{
    public class Plan
    {

        [Key]
        [Display(Name = "Plan ID")]
        [RegularExpression(@"[0-9]*", ErrorMessage = "Este campo solo acepta caracteres numéricos")]
        public int Id { get; set; }

        [Display(Name = "Precio")]
        [Required(ErrorMessage = "Este campo es requerido")]
        [RegularExpression(@"[0-9,]*", ErrorMessage = "Este campo solo acepta caracteres numéricos y comas")]
        public double Precio {  get; set; }

        [Display(Name = "Tipo de Plan")]
        public TipoPlan TipoPlan { get; set; }

        [Display(Name = "Tipo de Vehículo")]
        public TipoVehiculo TipoDeVehiculo { get; set; }
    }
}
