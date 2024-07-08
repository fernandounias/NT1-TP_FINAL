using System.ComponentModel.DataAnnotations;

namespace ParkingLotManagment.Models
{
    public class Cliente : Usuario
    {
        [Required(ErrorMessage = "Este campo es requerido")]
        [Display(Name = "Fecha de Nacimiento")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy H:mm}", NullDisplayText = "-")]
        public DateTime FechaDeNacimiento { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        [MaxLength (9, ErrorMessage = "Maxima longitud del campo son 9 digitos")]
        [Display(Name = "Documento Nacional de Identidad")]
        [RegularExpression(@"[0-9]*", ErrorMessage = "Este campo solo acepta caracteres numéricos")]
        public string? Dni { get; set; }

        [ScaffoldColumn(false)]
        public List<Vehiculo>? Vehiculos { get; set; }

    }
}
