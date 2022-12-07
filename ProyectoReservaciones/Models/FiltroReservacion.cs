using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProyectoReservaciones.Models
{
    public class FiltroReservacion
    {
        [DisplayName("Id Persona")]
        public int idPersona { get; set; }

        [Required]
        [DisplayName("Fecha de entrada")]
        [DataType(DataType.Date)]
        public DateTime fechaEntrada { get; set; }

        [Required]
        [DisplayName("Fecha de salida")]
        [DataType(DataType.Date)]
        public DateTime fechaSalida { get; set; }

    }
}