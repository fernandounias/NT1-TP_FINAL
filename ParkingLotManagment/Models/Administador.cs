using System.ComponentModel.DataAnnotations;

namespace ParkingLotManagment.Models
{
    public class Administador : Usuario
    {
        [Required(ErrorMessage = "Este campo es requerido")]
        [Display(Name = "N° de Legajo")]
        [RegularExpression(@"[a-zA-Z0-9]*", ErrorMessage = "Este campo solo acepta caracteres alfanuméricos")]
        [MaxLength(35, ErrorMessage = "Maxima longitud del campo son 35 caracteres al igual que un GUID")]
        public string Legajo { get; set; }

        [Display(Name = "Estacionamiento")]
        public Estacionamiento Estacionamiento { get; set; }
    }
}
