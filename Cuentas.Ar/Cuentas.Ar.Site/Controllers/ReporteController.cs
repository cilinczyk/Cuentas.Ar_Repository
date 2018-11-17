using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using Cuentas.Ar.Business;
using Cuentas.Ar.Entities;
using Cuentas.Ar.Site.Helpers;

namespace Cuentas.Ar.Site.Controllers
{
    public class ReporteController : Controller
    {
        public ActionResult Listado()
        {
            #region [Región: Actualizar Estados Objetivo
            var objetivoBusiness = new ObjetivoBusiness();

            int idUsuario = Convert.ToInt32(ClaimsPrincipal.Current.FindFirst(ClaimTypes.Sid).Value);
            objetivoBusiness.ActualizarEstados(idUsuario);
            #endregion

            return View("Listado");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public FileContentResult Registro(M_FiltroReporte filtroReporte, int idTipoReporte = 1)
        {
            #region [Región: Obtener Reporte]
            var registroBusiness = new RegistroBusiness();

            int idUsuario = Convert.ToInt32(ClaimsPrincipal.Current.FindFirst(ClaimTypes.Sid).Value);
            var reporte = registroBusiness.ListarRegistros(idUsuario, filtroReporte.FechaDesde ?? DateTime.Now.AddYears(-10), DateTime.Now.AddYears(10));
            #endregion

            //if (idTipoReporte == 1)
            //{
            #region [Región: Parsear Registro - Excel]
            List<M_RegistroExcel> listaRegistroExcel = new List<M_RegistroExcel>();
            foreach (var item in reporte)
            {
                M_RegistroExcel registroExcel = new M_RegistroExcel
                {
                    Importe = string.Format(new System.Globalization.CultureInfo("es-AR"), "{0:N2}", item.Importe),
                    Fecha = item.Fecha.ToShortDateString()
                };

                listaRegistroExcel.Add(registroExcel);
            }
            #endregion

            #region [Región: Exportar Excel]
            byte[] filecontent = ReporteRegistro(listaRegistroExcel);

                return File(filecontent, ExcelExportHelper.ExcelContentType, string.Format("{0}.xlsx", "MisCuentasReporte_Registros"));
                #endregion
            //}
        }

        private byte[] ReporteRegistro(List<M_RegistroExcel> reporte)
        {
            try
            {
                M_Excel excelNew = new M_Excel();
                M_Worksheet workSheet = new M_Worksheet
                {
                    Header = "Reporte de Registros",
                    Data = DTHelper.ToDataTable(reporte),
                };

                workSheet.Columns.Add(new M_Column(0, "Importe", "Importe"));
                workSheet.Columns.Add(new M_Column(1, "Fecha", "Fecha", typeof(DateTime)));

                excelNew.WorksheetList.Add(workSheet);

                return ExcelExportHelper.ExportExcel(excelNew);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}