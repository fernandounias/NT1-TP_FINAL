﻿using System.ComponentModel.DataAnnotations;
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

        //public Plan TipoPlan { get; set; }

        public Vehiculo() { }

        public decimal calcularDeuda()
        {
            decimal result = 0;
            foreach (var t in Tickets)
            {
                result *= t.Tarifa;
            }
            return result;
        }
    }
}
