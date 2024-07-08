using System.ComponentModel.DataAnnotations;
using System.Data;

namespace ParkingLotManagment.Models
{
    public class Ticket
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        [Display(Name = "Fecha y Hora de Ingreso")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy H:mm}", NullDisplayText = "-")]
        public DateTime Ingreso { get; set; }

        [Display(Name = "Fecha y Hora de Salida")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy H:mm}", NullDisplayText = "-")]
        public DateTime Salida { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        [Display(Name = "Patente")]
        public string? Patente { get; set; }

        [Display(Name = "Tarifa")]
        public decimal? Tarifa { get; set; }

        [Display(Name = "Moneda")]
        public string? Moneda { get; set; }


    }
        
}
