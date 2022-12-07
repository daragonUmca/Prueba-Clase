using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProyectoReservaciones.Models;
using ProyectoReservaciones.Data;

namespace ProyectoReservaciones.Controllers
{
    public class HotelController : Controller
    {
        // GET: Hotel
        public List<HotelModel> listarHotel()
        {
            List<HotelModel> listaHotel = new List<HotelModel>();

            using (ReservacionesDBEntities db = new ReservacionesDBEntities())
            {
                listaHotel = (from ti in db.spConsultaHotelFiltro()
                                select new HotelModel
                                {
                                    idHotel = ti.idHotel,
                                    nombre = ti.nombre
                                }).ToList();

                return listaHotel;
            }
        }
    }
}