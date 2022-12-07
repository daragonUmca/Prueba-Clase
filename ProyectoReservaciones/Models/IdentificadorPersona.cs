using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProyectoReservaciones.Models
{
    public class IdentificadorPersona
    {
        [Required]
        [DisplayName("Id Persona")]
        public int idPersona { get; set; }

        [Required]
        [DisplayName("Nombre completo")]
        public string nombreCompleto { get; set; }

    }
}