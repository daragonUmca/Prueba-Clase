using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProyectoReservaciones.Models
{
    public class ReservacionModel
    {
        [Required]
        [DisplayName("Id Reservación")]
        public int idReservacion { get; set; }

        [Required]
        [DisplayName("Id Persona")]
        public int idPersona { get; set; }

        [Required]
        [DisplayName("Id Habitación")]
        public int idHabitacion { get; set; }

        [DisplayName("Número de habitación")]
        public string numHabitacion { get; set; }

        [Required]
        [DisplayName("Fecha de entrada")]
        [DataType(DataType.Date)]
        public DateTime fechaEntrada { get; set; }

        [Required]
        [DisplayName("Fecha de salida")]
        [DataType(DataType.Date)]
        public DateTime fechaSalida { get; set; }

        [Required]
        [DisplayName("Número adultos")]
        public int numeroAdultos { get; set; }

        [Required]
        [DisplayName("Número niños")]
        public int numeroNinhos { get; set; }

        [Required]
        [DisplayName("Total días reservación")]
        public int totalDiasReservacion { get; set; }

        [Required]
        [DisplayName("Costo por adulto")]
        public decimal costoPorAdulto { get; set; }

        [Required]
        [DisplayName("Costo por niño")]
        public decimal costoPorNinho { get; set; }

        [Required]
        [DisplayName("Costo total")]
        public decimal costoTotal { get; set; }

        [Required]
        [DisplayName("Fecha de creación")]
        [DataType(DataType.Date)]
        public DateTime fechaCreacion { get; set; }

        [DisplayName("Fecha de modificación")]
        [DataType(DataType.Date)]
        public DateTime? fechaModificacion { get; set; }

        [Required]
        [DisplayName("Estado reservación")]
        public string estado { get; set; }

        [Required]
        [DisplayName("Hotel")]
        public string hotel { get; set; }

        [Required]
        [DisplayName("Nombre Completo")]
        public string nombreCompleto { get; set; }

        [DisplayName("Id Hotel")]
        public int? idHotel { get; set; }
    }
}