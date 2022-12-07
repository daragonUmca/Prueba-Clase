using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProyectoReservaciones.Models
{
    public class BitacoraModel
    {
        [DisplayName("Id Reservación")]
        public int? idReservacion { get; set; }

        [Required]
        [DisplayName("Fecha")]
        [DataType(DataType.Date)]
        public DateTime fechaAccion { get; set; }

        [DisplayName("Acción realizada")]
        public string accion { get; set; }

        [Required]
        [DisplayName("Realizado por")]
        public string persona { get; set; }

    }
}