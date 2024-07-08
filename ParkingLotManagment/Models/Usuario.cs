using System.ComponentModel.DataAnnotations;

namespace ParkingLotManagment.Models
{
    public abstract class Usuario
    {
        [Key]
        public Guid Id { get; set; }
        
        [Required (ErrorMessage = "Este campo es requerido")]
        [MaxLength (50, ErrorMessage = "Maxima longitud del campo son 50 caracteres")]
        [RegularExpression(@"[a-zA-Z áéíóú]*", ErrorMessage = "Este campo solo acepta caracteres alfabéticos")]
        [Display(Name = "Nombre")]
        public string? Nombre { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        [MaxLength(50, ErrorMessage = "Maxima longitud del campo son 50 caracteres")]
        [RegularExpression(@"[a-zA-Z áéíóú]*", ErrorMessage = "Este campo solo acepta caracteres alfabéticos")]
        [Display(Name = "Apellido")]
        public string? Apellido { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        [MaxLength(50, ErrorMessage = "Maxima longitud del campo son 50 caracteres")]
        [RegularExpression(@"[a-zA-Z0-9áéíóú_]*", ErrorMessage = "Este campo solo acepta caracteres alfanuméricos y _")]
        [Display(Name = "Nombre de Usuario")]
        public string? Username { get; set; }

        [ScaffoldColumn(false)]
        public byte[] Password { get; set; }

        [ScaffoldColumn(false)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy H:mm}", NullDisplayText = "-")]
        [Display(Name = "Fecha de Alta")]
        public DateTime FechaAlta { get; set; }

        [ScaffoldColumn(false)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy H:mm}", NullDisplayText = "-")]
        [Display(Name = "Ultimo Acceso")]
        public DateTime? FechaUltimoAcceso { get; set; }
        
    }
}
