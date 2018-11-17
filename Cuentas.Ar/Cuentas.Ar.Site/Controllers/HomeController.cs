using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using Cuentas.Ar.Business;
using Cuentas.Ar.Entities;

namespace Cuentas.Ar.Site.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            int idUsuario = Convert.ToInt32(ClaimsPrincipal.Current.FindFirst(ClaimTypes.Sid).Value);

            #region [Región: Actualizar Estados Objetivo
            var objetivoBusiness = new ObjetivoBusiness();
            objetivoBusiness.ActualizarEstados(idUsuario);
            #endregion

            return View();
        }

        [HttpPost]
        public JsonResult RefrescarGraficoRegistros()
        {
            try
            {
                var registroBusiness = new RegistroBusiness();
                int idUsuario = Convert.ToInt32(ClaimsPrincipal.Current.FindFirst(ClaimTypes.Sid).Value);

                #region [Región: Declaraciones]
                int ingresos = 0;
                int gastos = 0;
                int ahorros = 0;

                bool estadoGrafico = false;
                List<string> data = new List<string>();
                List<string> labels = new List<string>();
                #endregion

                #region [Región: Labels]
                labels.Add("INGRESOS");
                labels.Add("GASTOS");
                labels.Add("AHORROS");
                #endregion

                #region [Región: Data]
                var fechaDesde = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                var fechaHasta = fechaDesde.AddMonths(1).AddDays(-1);

                var listaRegistros = registroBusiness.Listar(idUsuario, fechaDesde, fechaHasta);
                ingresos = (int) listaRegistros.Where(x => x.idTipoRegistro == eTipoRegistro.Ingreso).Sum(x => x.Importe);
                gastos = (int) listaRegistros.Where(x => x.idTipoRegistro == eTipoRegistro.Gasto).Sum(x => x.Importe);
                ahorros = (int) listaRegistros.Where(x => x.idCategoria == eCategoria.Ahorros).Sum(x => x.Importe);

                data.Add(ingresos.ToString());
                data.Add(gastos.ToString());
                data.Add(ahorros.ToString());

                estadoGrafico = (ingresos + gastos + ahorros) > 0 ? true : false;
                #endregion

                return new JsonCamelCaseResult(new AppResponse<object>
                {
                    Data = new
                    {
                        Success = estadoGrafico,
                        labels = labels,
                        data = data
                    }
                });
            }
            catch (Exception ex)
            {
                return new JsonCamelCaseResult(new AppResponse<object>
                {
                    Data = new
                    {
                        Success = false,
                        Message = ex.Message.ToString()
                    }
                });
            }
        }

        public JsonResult RefrescarGraficoFlujo()
        {
            try
            {
                var registroBusiness = new RegistroBusiness();
                int idUsuario = Convert.ToInt32(ClaimsPrincipal.Current.FindFirst(ClaimTypes.Sid).Value);

                #region [Región: Declaraciones]
                int ingresos = 0;
                int gastos = 0;
                int neto = 0;

                bool estadoGrafico = true;
                List<string> data = new List<string>();
                List<string> labels = new List<string>();
                #endregion

                
                #region [Región: Data]
                var fechaDesde = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                var fechaHasta = fechaDesde.AddMonths(1).AddDays(-1);

                var listaRegistros = registroBusiness.Listar(idUsuario, fechaDesde, fechaHasta);

                #region [Región: Labels]
                List<string> listaDias = listaRegistros.Select(x => x.Fecha.ToString("dd/MM")).Distinct().ToList();

                foreach (var diaMes in listaDias)
                {
                    labels.Add(diaMes);

                    ingresos = (int)listaRegistros.Where(x => x.idTipoRegistro == eTipoRegistro.Ingreso && x.Fecha.ToString("dd/MM") == diaMes).Sum(x => x.Importe);
                    gastos = (int)listaRegistros.Where(x => x.idTipoRegistro == eTipoRegistro.Gasto && x.Fecha.ToString("dd/MM") == diaMes).Sum(x => x.Importe);
                    neto = ingresos - gastos;

                    data.Add(neto.ToString());
                }
                #endregion

                #endregion

                return new JsonCamelCaseResult(new AppResponse<object>
                {
                    Data = new
                    {
                        Success = estadoGrafico,
                        labels = labels,
                        data = data
                    }
                });
            }
            catch (Exception ex)
            {
                return new JsonCamelCaseResult(new AppResponse<object>
                {
                    Data = new
                    {
                        Success = false,
                        Message = ex.Message.ToString()
                    }
                });
            }
        }
    }
}