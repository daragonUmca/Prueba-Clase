using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProyectoReservaciones.Models;
using ProyectoReservaciones.Data;
using ProyectoReservaciones.Controllers;

namespace ProyectoReservaciones.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Login()
        {
            return View("Login");
        }

        [HttpGet]
        public ActionResult IniciarSesion(PersonaModel modelo)
        {
            try
            {
                PersonaModel persona = null;
                using (ReservacionesDBEntities reservacionesDB = new ReservacionesDBEntities())
                {
                    persona = (from p in reservacionesDB.spConsultaUsuario(modelo.email, modelo.clave)
                    select new PersonaModel
                               {
                                   idPersona = p.idPersona,
                                   nombreCompleto = p.nombreCompleto,
                                   email = p.email,
                                   clave = p.clave,
                                   esEmpleado = p.esEmpleado,
                                   estado = p.estado
                               }).FirstOrDefault();
                }

                if (!(persona is null))
                {
                    if (persona.estado == "A")
                    {
                        Session["Persona"] = persona;
                        Session["TipoUsuario"] = persona.esEmpleado;
                        Session["IdPersona"] = persona.idPersona;
                        Session["NombreCompleto"] = persona.nombreCompleto;
                        ViewBag.EsEmpleado = persona.esEmpleado;
                        ViewBag.NombreCompleto = persona.nombreCompleto;
                        if ((bool)Session["TipoUsuario"] == true)
                        {
                            return RedirectToAction("GestionarReservaciones", "Reservacion");
                        }
                        else {
                            return RedirectToAction("ListaReservaciones", "Reservacion");
                        }
                        
                        /*
                        bool session =(bool)Session["TipoUsuario"];
                        ReservacionController reservacion = new ReservacionController();
                        reservacion.ListaReservaciones();*/
                    }
                    else if (persona.estado == "I")
                    {
                        ViewBag.Message = "El usuario ingresado está inactivo";
                        return View("Login");
                    }
                    else {
                        ViewBag.Message = "Datos incorrectos";
                        return View("Login");
                    }
                }
                else {
                    ViewBag.Message = "Usuario o contraseña incorrectos";
                    return View("Login");
                }
                               
            }
            catch
            {
                throw;
            }
        }


        public ActionResult CerrarSesion()
        {

            Session.RemoveAll();
            return View("Login");
        }


    }
}