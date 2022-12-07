using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProyectoReservaciones.Models;
using ProyectoReservaciones.Data;

namespace ProyectoReservaciones.Controllers
{
    public class UsuarioIdentificadorController : Controller
    {
        // GET: UsuarioIdentificador
        public List<IdentificadorPersona> listarCliente()
        {
            List<IdentificadorPersona> listaCliente = new List<IdentificadorPersona>();

            using (ReservacionesDBEntities db = new ReservacionesDBEntities())
            {
                listaCliente = (from ti in db.spConsultaUsuarioFiltro()
                                  select new IdentificadorPersona
                                  {
                                      idPersona = ti.idPersona,
                                      nombreCompleto = ti.nombreCompleto
                                  }).ToList();

                return listaCliente;
            }
        }
    }
}