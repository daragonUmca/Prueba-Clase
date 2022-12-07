using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProyectoReservaciones.Models;
using ProyectoReservaciones.Data;

namespace ProyectoReservaciones.Controllers
{
    public class ReservacionController : Controller
    {
        [HttpGet]
        public ActionResult ListaReservaciones()
        {
            if (Session["Persona"] == null) {
                return View("~/Views/Login/Login.cshtml");
            }
            bool esEmpleado = (bool)Session["TipoUsuario"];
            ViewBag.esEmpleado = esEmpleado;   

                try
                {
                    int idPersona = (int)Session["IdPersona"];
                    List<ReservacionModel> listaReservaciones = new List<ReservacionModel>();
                    using (ReservacionesDBEntities reservacionDB = new ReservacionesDBEntities())
                    {
                        listaReservaciones = (from p in reservacionDB.spConsultarReservacionesClienteID(idPersona)
                                              select new ReservacionModel
                                              {
                                                  idReservacion = p.idReservacion,
                                                  hotel = p.nombre,
                                                  fechaEntrada = p.fechaEntrada,
                                                  fechaSalida = p.fechaSalida,
                                                  costoTotal = p.costoTotal,
                                                  estado = p.estado
                                              }).ToList();
                    }
                    return View("ListaReservaciones", listaReservaciones);

                }
                catch (Exception)
                {
                    throw;
                }
        }





        [HttpGet]
        public ActionResult GestionarReservaciones()
        {
            if (Session["Persona"] == null)
            {
                return View("~/Views/Login/Login.cshtml");
            }
            bool esEmpleado = (bool)Session["TipoUsuario"];
            ViewBag.esEmpleado = esEmpleado;

            try
            {
                int idPersona = (int)Session["IdPersona"];
                List<ReservacionModel> listaReservaciones = new List<ReservacionModel>();
                using (ReservacionesDBEntities reservacionDB = new ReservacionesDBEntities())
                {
                    listaReservaciones = (from p in reservacionDB.spConsultarReservacionesEmpleadoExcluir(idPersona)
                                          select new ReservacionModel
                                          {
                                              idReservacion = p.idReservacion,
                                              nombreCompleto = p.nombreCompleto,
                                              hotel = p.nombre,
                                              fechaEntrada = p.fechaEntrada,
                                              fechaSalida = p.fechaSalida,
                                              costoTotal = p.costoTotal,
                                              estado = p.estado
                                          }).ToList();
                }


                UsuarioIdentificadorController listaCliente = new UsuarioIdentificadorController();
                ViewBag.ListaCliente = listaCliente.listarCliente();

                FiltroReservacion filtro = new FiltroReservacion();
                ViewBag.FiltroReservacion = filtro;

                return View("GestionarReservaciones", listaReservaciones);

            }
            catch (Exception)
            {
                throw;
            }
        }








        [HttpPost]
        public ActionResult GestionarReservaciones(int idCliente, DateTime fechaEntrada, DateTime fechaSalida)
        {
            if (Session["Persona"] == null)
            {
                return View("~/Views/Login/Login.cshtml");
            }
            bool esEmpleado = (bool)Session["TipoUsuario"];
            ViewBag.esEmpleado = esEmpleado;

            try
            {
                List<ReservacionModel> listaReservaciones = new List<ReservacionModel>();
                using (ReservacionesDBEntities reservacionDB = new ReservacionesDBEntities())
                {
                    listaReservaciones = (from p in reservacionDB.spConsultarReservacionesEmpleadoFiltrado(idCliente, fechaEntrada, fechaSalida)
                                          select new ReservacionModel
                                          {
                                              idReservacion = p.idReservacion,
                                              nombreCompleto = p.nombreCompleto,
                                              hotel = p.nombre,
                                              fechaEntrada = p.fechaEntrada,
                                              fechaSalida = p.fechaSalida,
                                              costoTotal = p.costoTotal,
                                              estado = p.estado
                                          }).ToList();
                }


                UsuarioIdentificadorController listaCliente = new UsuarioIdentificadorController();
                ViewBag.ListaCliente = listaCliente.listarCliente();

                return View("GestionarReservaciones", listaReservaciones);

            }
            catch (Exception)
            {
                throw;
            }
        }








        [HttpGet]
        public ActionResult DetalleReservacion(int idReservacion)
        {
            if (Session["Persona"] == null)
            {
                return View("~/Views/Login/Login.cshtml");
            }
            bool esEmpleado = (bool)Session["TipoUsuario"];
            ViewBag.esEmpleado = esEmpleado;
            Session["idReservacion"] = idReservacion;

            if (esEmpleado == true)
            {
                try
                {
                    ReservacionModel reservacion = null;
                    using (ReservacionesDBEntities reservacionDB = new ReservacionesDBEntities())
                    {
                        reservacion = (from p in reservacionDB.spConsultarReservacionID(idReservacion)
                                       select new ReservacionModel
                                       {
                                           idReservacion = p.idReservacion,
                                           hotel = p.nombre,
                                           numHabitacion = p.numeroHabitacion,
                                           nombreCompleto = p.nombreCompleto,
                                           fechaEntrada = p.fechaEntrada,
                                           fechaSalida = p.fechaSalida,
                                           numeroAdultos = p.numeroAdultos,
                                           numeroNinhos = p.numeroNinhos,
                                           costoTotal = p.costoTotal,
                                           estado = p.estado
                                       }).FirstOrDefault();
                    }

                    return View("DetalleReservacion", reservacion);
                }
                catch
                {
                    return View("_Error");
                }
            }
            else {
                    try
                    {
                        ReservacionModel reservacion = null;
                        using (ReservacionesDBEntities reservacionDB = new ReservacionesDBEntities())
                        {
                            reservacion = (from p in reservacionDB.spConsultarReservacionID(idReservacion)
                                           select new ReservacionModel
                                           {
                                               idReservacion = p.idReservacion,
                                               hotel = p.nombre,
                                               numHabitacion = p.numeroHabitacion,
                                               nombreCompleto = p.nombreCompleto,
                                               fechaEntrada = p.fechaEntrada,
                                               fechaSalida = p.fechaSalida,
                                               numeroAdultos = p.numeroAdultos,
                                               numeroNinhos = p.numeroNinhos,
                                               costoTotal = p.costoTotal,
                                               idPersona = p.idPersona,
                                               estado = p.estado
                                           }).FirstOrDefault();
                        }

                    if (reservacion.idPersona == (int)Session["IdPersona"]) {
                        return View("DetalleReservacion", reservacion);
                    }

                    else
                    {
                        return Redirect("ListaReservaciones");
                    }
                    }
                    catch
                    {
                        return View("_Error");
                    }
            }

        }


        [HttpGet]
        public ActionResult Bitacora()
        {
            if (Session["Persona"] == null)
            {
                return View("~/Views/Login/Login.cshtml");
            }
            bool esEmpleado = (bool)Session["TipoUsuario"];
            ViewBag.esEmpleado = esEmpleado;
            int idReservacion = (int)Session["idReservacion"];
            try
            {
                List<BitacoraModel> listaBitacora = new List<BitacoraModel>();
                using (ReservacionesDBEntities bitacoraDB = new ReservacionesDBEntities())
                {
                    listaBitacora = (from p in bitacoraDB.spConsultarBitacoraID(idReservacion)
                                          select new BitacoraModel
                                          {
                                              fechaAccion = p.fechaDeLaAccion,
                                              accion = p.accionRealizada,
                                              persona = p.nombreCompleto
                                          }).ToList();
                }

                return View("Bitacora", listaBitacora);
            }
            catch
            {
                return View("_Error");
            }
        }






        [HttpGet]
        public ActionResult CrearReservacion()
        {
            if (Session["Persona"] == null)
            {
                return View("~/Views/Login/Login.cshtml");
            }
            bool esEmpleado = (bool)Session["TipoUsuario"];
            ViewBag.esEmpleado = esEmpleado;


            HotelController listaHotel = new HotelController();
            ViewBag.listaHotel = listaHotel.listarHotel();

            UsuarioIdentificadorController listaCliente = new UsuarioIdentificadorController();
            List<IdentificadorPersona> clientes = new List<IdentificadorPersona>();
            List<IdentificadorPersona> clienteSeleccionado = new List<IdentificadorPersona>();
            clientes = listaCliente.listarCliente();

            if (!(esEmpleado))
            {
                foreach (var client in clientes)
                {
                    if (client.idPersona == (int)Session["IdPersona"])
                    {
                        clienteSeleccionado.Add(client);
                    }
                }
                ViewBag.ListaCliente = clienteSeleccionado;
            }
            else {
                ViewBag.ListaCliente = clientes;
            }


            return View("CrearReservacion");
        }






        
        [HttpPost]
        public ActionResult CrearReservacion(ReservacionModel modelo)
        {
            if (Session["Persona"] == null)
            {
                return View("~/Views/Login/Login.cshtml");
            }
            bool esEmpleado = (bool)Session["TipoUsuario"];
            ViewBag.esEmpleado = esEmpleado;


            HotelController listaHotel = new HotelController();
            ViewBag.listaHotel = listaHotel.listarHotel();


            UsuarioIdentificadorController listaCliente = new UsuarioIdentificadorController();
            List<IdentificadorPersona> clientes = new List<IdentificadorPersona>();
            List<IdentificadorPersona> clienteSeleccionado = new List<IdentificadorPersona>();
            clientes = listaCliente.listarCliente();

            if (!(esEmpleado))
            {
                foreach (var client in clientes)
                {
                    if (client.idPersona == (int)Session["IdPersona"])
                    {
                        clienteSeleccionado.Add(client);
                    }
                }
                ViewBag.ListaCliente = clienteSeleccionado;
            }
            else
            {
                ViewBag.ListaCliente = clientes;
            }



            try
            {
                try
                {
                    ReservacionModel reservacion = null;
                    using (ReservacionesDBEntities reservacionDB = new ReservacionesDBEntities())
                    {
                        reservacion = (from p in reservacionDB.spConsultarHabitacionDisponible((modelo.numeroAdultos + modelo.numeroNinhos), modelo.idHotel)
                                       select new ReservacionModel
                                       {
                                           idHabitacion = p.idHabitacion
                                       }).FirstOrDefault();
                    }

                    if (reservacion == null)
                    {
                        ViewBag.errorHabitacion = "Sin habitaciones disponibles. Cambiar el número de personas para la reservación";
                        return Redirect("CrearReservacion");
                    }

                    modelo.idHabitacion = reservacion.idHabitacion;

                    using (ReservacionesDBEntities reservacionesDB = new ReservacionesDBEntities())
                    {
                        reservacionesDB.spCrearReservacion(modelo.idPersona, modelo.idHabitacion, modelo.fechaEntrada, modelo.fechaSalida, modelo.numeroAdultos, modelo.numeroNinhos,  DateTime.Now);
                        reservacion = (from p in reservacionesDB.spConsultarUltimaReservacion()
                                       select new ReservacionModel
                                       {
                                           idReservacion = (int)p
                                       }).FirstOrDefault();
                        modelo.idReservacion = reservacion.idReservacion;
                        reservacionesDB.spInsertarBitacora(modelo.idReservacion, (int)Session["IdPersona"], "CREADA", DateTime.Now);

                    }

                }
                catch
                {
                    return View("Error");
                }

                ViewBag.mensajeResultado = "Se ha ingresado la reservación correctamente";
                return View("Resultado");
            }
            catch
            {
                return View("Error");
            }
        }






        [HttpGet]
        public ActionResult EditarReservacion(int idReservacion)
        {
            if (Session["Persona"] == null)
            {
                return View("~/Views/Login/Login.cshtml");
            }
            bool esEmpleado = (bool)Session["TipoUsuario"];
            ViewBag.esEmpleado = esEmpleado;
            Session["idReservacion"] = idReservacion;

            if (esEmpleado == true)
            {
                try
                {
                    ReservacionModel reservacion = null;
                    using (ReservacionesDBEntities reservacionDB = new ReservacionesDBEntities())
                    {
                        reservacion = (from p in reservacionDB.spConsultarReservacionID(idReservacion)
                                       select new ReservacionModel
                                       {
                                           idReservacion = p.idReservacion,
                                           hotel = p.nombre,
                                           numHabitacion = p.numeroHabitacion,
                                           nombreCompleto = p.nombreCompleto,
                                           fechaEntrada = p.fechaEntrada,
                                           fechaSalida = p.fechaSalida,
                                           numeroAdultos = p.numeroAdultos,
                                           numeroNinhos = p.numeroNinhos,
                                           costoTotal = p.costoTotal,
                                           estado = p.estado,
                                           idHabitacion = p.idHabitacion
                                       }).FirstOrDefault();
                    }
                    Session["idHabitacion"] = reservacion.idHabitacion;

                    return View("EditarReservacion", reservacion);
                }
                catch
                {
                    return View("_Error");
                }
            }
            else
            {
                try
                {
                    ReservacionModel reservacion = null;
                    using (ReservacionesDBEntities reservacionDB = new ReservacionesDBEntities())
                    {
                        reservacion = (from p in reservacionDB.spConsultarReservacionID(idReservacion)
                                       select new ReservacionModel
                                       {
                                           idReservacion = p.idReservacion,
                                           hotel = p.nombre,
                                           numHabitacion = p.numeroHabitacion,
                                           nombreCompleto = p.nombreCompleto,
                                           fechaEntrada = p.fechaEntrada,
                                           fechaSalida = p.fechaSalida,
                                           numeroAdultos = p.numeroAdultos,
                                           numeroNinhos = p.numeroNinhos,
                                           costoTotal = p.costoTotal,
                                           idPersona = p.idPersona,
                                           estado = p.estado
                                       }).FirstOrDefault();
                    }

                    if (reservacion.idPersona == (int)Session["IdPersona"])
                    {
                        return View("EditarReservacion", reservacion);
                    }

                    else
                    {
                        return Redirect("ListaReservaciones");
                    }
                }
                catch
                {
                    return View("_Error");
                }
            }

        }







        [HttpPost]
        public ActionResult EditarReservacion(ReservacionModel modelo)
        {
            if (Session["Persona"] == null)
            {
                return View("~/Views/Login/Login.cshtml");
            }
            bool esEmpleado = (bool)Session["TipoUsuario"];
            ViewBag.esEmpleado = esEmpleado;


            if (esEmpleado == true & modelo.estado == "I")
            {
                return Redirect("GestionarReservaciones");
            }
            else if (esEmpleado == false & modelo.estado == "I") {
                return Redirect("ListaReservaciones");
            }
            else if (esEmpleado == true & modelo.fechaSalida <= DateTime.Now) {
                return Redirect("GestionarReservaciones");
            }
            else if (esEmpleado == false & modelo.fechaSalida <= DateTime.Now) {
                return Redirect("ListaReservaciones");
            }
            else if (esEmpleado == false & modelo.fechaEntrada <= DateTime.Now & modelo.fechaSalida > DateTime.Now) {
                return Redirect("ListaReservaciones");
            }

            modelo.idHabitacion = (int)Session["idHabitacion"];

            ReservacionModel reservacion = null;
            using (ReservacionesDBEntities reservacionDB = new ReservacionesDBEntities())
            {
                reservacion = (from p in reservacionDB.spConsultarHabitacionDisponibleID((modelo.numeroAdultos + modelo.numeroNinhos), modelo.idHabitacion)
                               select new ReservacionModel
                               {
                                   idHabitacion = p.idHabitacion
                               }).FirstOrDefault();
            }

            if (reservacion == null)
            {
                ViewBag.errorHabitacion = "La habitación reservada no tiene la capacidad suficente. Cambiar el número de personas para la reservación";
                return Redirect("EditarReservacion");
            }



            try
            {
                using (ReservacionesDBEntities reservacionesDB = new ReservacionesDBEntities())
                {
                    reservacionesDB.spEditarReservacion(modelo.idReservacion, modelo.fechaEntrada, modelo.fechaSalida, modelo.numeroAdultos, modelo.numeroNinhos, DateTime.Now);
                    reservacionesDB.spInsertarBitacora(modelo.idReservacion, (int)Session["IdPersona"], "CORREGIDA", DateTime.Now);
                }
                ViewBag.mensajeResultado = "Se han actualizado los datos de la reservación.";

                return View("Resultado");
            }
            catch
            {
                return View("_Error");
            }
        }





        [HttpGet]
        public ActionResult CancelarReservacion(int idReservacion)
        {
            if (Session["Persona"] == null)
            {
                return View("~/Views/Login/Login.cshtml");
            }
            bool esEmpleado = (bool)Session["TipoUsuario"];
            ViewBag.esEmpleado = esEmpleado;
            Session["idReservacion"] = idReservacion;

            if (esEmpleado == true)
            {
                try
                {
                    ReservacionModel reservacion = null;
                    using (ReservacionesDBEntities reservacionDB = new ReservacionesDBEntities())
                    {
                        reservacion = (from p in reservacionDB.spConsultarReservacionID(idReservacion)
                                       select new ReservacionModel
                                       {
                                           idReservacion = p.idReservacion,
                                           hotel = p.nombre,
                                           numHabitacion = p.numeroHabitacion,
                                           nombreCompleto = p.nombreCompleto,
                                           fechaEntrada = p.fechaEntrada,
                                           fechaSalida = p.fechaSalida,
                                           numeroAdultos = p.numeroAdultos,
                                           numeroNinhos = p.numeroNinhos,
                                           costoTotal = p.costoTotal,
                                           estado = p.estado,
                                           idHabitacion = p.idHabitacion
                                       }).FirstOrDefault();
                    }
                    Session["idHabitacion"] = reservacion.idHabitacion;

                    if (reservacion.estado == "I")
                    {
                        return Redirect("GestionarReservaciones");
                    }
                    else if (reservacion.fechaSalida <= DateTime.Now)
                    {
                        return Redirect("GestionarReservaciones");
                    }
                    else if (reservacion.fechaEntrada <= DateTime.Now & reservacion.fechaEntrada > DateTime.Now) {
                        return Redirect("GestionarReservaciones");
                    }



                    using (ReservacionesDBEntities reservacionesDB = new ReservacionesDBEntities())
                    {
                        reservacionesDB.spCancelarReservacion(reservacion.idReservacion, DateTime.Now);
                        reservacionesDB.spInsertarBitacora(reservacion.idReservacion, (int)Session["IdPersona"], "CANCELADA", DateTime.Now);
                    }
                    ViewBag.mensajeResultado = "Se ha cancelado la reservación.";

                    return View("Resultado");
                }
                catch
                {
                    return View("_Error");
                }
            }
            else
            {
                try
                {
                    ReservacionModel reservacion = null;
                    using (ReservacionesDBEntities reservacionDB = new ReservacionesDBEntities())
                    {
                        reservacion = (from p in reservacionDB.spConsultarReservacionID(idReservacion)
                                       select new ReservacionModel
                                       {
                                           idReservacion = p.idReservacion,
                                           hotel = p.nombre,
                                           numHabitacion = p.numeroHabitacion,
                                           nombreCompleto = p.nombreCompleto,
                                           fechaEntrada = p.fechaEntrada,
                                           fechaSalida = p.fechaSalida,
                                           numeroAdultos = p.numeroAdultos,
                                           numeroNinhos = p.numeroNinhos,
                                           costoTotal = p.costoTotal,
                                           idPersona = p.idPersona,
                                           estado = p.estado
                                       }).FirstOrDefault();
                    }

                    if (reservacion.idPersona == (int)Session["IdPersona"])
                    {
                        if (reservacion.estado == "I") {
                            return Redirect("ListaReservaciones");
                        }
                        else if (reservacion.fechaSalida <= DateTime.Now) {
                            return Redirect("ListaReservaciones");
                        }
                        else if (reservacion.fechaEntrada <= DateTime.Now & reservacion.fechaEntrada > DateTime.Now)
                        {
                            return Redirect("ListaReservaciones");
                        }

                        using (ReservacionesDBEntities reservacionesDB = new ReservacionesDBEntities())
                        {
                            reservacionesDB.spCancelarReservacion(reservacion.idReservacion, DateTime.Now);
                            reservacionesDB.spInsertarBitacora(reservacion.idReservacion, (int)Session["IdPersona"], "CANCELADA", DateTime.Now);
                        }
                        ViewBag.mensajeResultado = "Se ha cancelado la reservación.";

                        return View("Resultado");
                    }

                    else
                    {
                        return Redirect("ListaReservaciones");
                    }
                }
                catch
                {
                    return View("_Error");
                }
            }

        }



        public ActionResult CerrarSesion()
        {

            Session.RemoveAll();
            return View("~/Views/Login/Login.cshtml");
        }
    }
}