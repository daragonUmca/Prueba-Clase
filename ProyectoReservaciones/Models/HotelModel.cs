using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProyectoReservaciones.Models
{
    public class HotelModel
    {
        [Required]
        [DisplayName("Hotel")]
        public int idHotel { get; set; }

        [Required]
        [DisplayName("Nombre Hotel")]
        public string nombre { get; set; }

    }
}