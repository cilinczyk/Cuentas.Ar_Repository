using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Cuentas.Ar.Business;
using Cuentas.Ar.Entities;

namespace Cuentas.Ar.Site.Controllers
{
    public class LocalidadController : Controller
    {
        public JsonResult ListarLocalidades(int idProvincia)
        {
            try
            {
                if (idProvincia != 0)
                {
                    List<Localidad> ddlLocalidades = new LocalidadBusiness().Listar(idProvincia);

                    if (ddlLocalidades.Count > 0)
                    {
                        return this.Json(new { Estado = 1, Combo = new SelectList(ddlLocalidades.ToArray(), "idLocalidad", "Descripcion") }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return this.Json(new { Estado = 0, Mensaje = "No se han encontrado localidades para la provincia seleccionada." }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return this.Json(new { Estado = 0, Mensaje = "No se ha enviado una provincia." }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception)
            {
                return this.Json(new { Estado = 0, Mensaje = "Se ha encontrado un error al cargar el listado de localidades." }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}