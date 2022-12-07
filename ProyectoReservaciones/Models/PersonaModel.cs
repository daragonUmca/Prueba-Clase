using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProyectoReservaciones.Models
{
    public class PersonaModel
    {
        
        [Required]
        [DisplayName("Id Persona")]
        public int idPersona { get; set; }

        [Required]
        [DisplayName("Nombre completo")]
        public string nombreCompleto { get; set; }
        
        [Required]
        [DisplayName("Correo eletrónico")]
        [EmailAddress(ErrorMessage = "Dirección de correo inválida")]
        public string email { get; set; }

        [Required]
        [DisplayName("Contraseña")]
        public string clave { get; set; }
        
        [Required]
        [DisplayName("Es empleado")]
        public bool esEmpleado { get; set; }

        [Required]
        [DisplayName("Estado")]
        public string estado { get; set; }
        
    }
}