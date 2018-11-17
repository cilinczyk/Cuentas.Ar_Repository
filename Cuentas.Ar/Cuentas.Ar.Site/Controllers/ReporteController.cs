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

        public FileContentResult Registro()
        {
            #region [Región: Obtener Reporte]
            RPT_Reportes report = TempData["Reporte"] as RPT_Reportes;
            report.ReporteBase.idOrganizador = UsuarioLogin.GetOrganizadorID();
            RPT_Reporte rptReporte = _reporteBusiness.ObtenerPorId(report.ReporteBase.id_Reporte);

            Type tipo = typeof(RPT_ReporteBusiness);
            object classInstance = Activator.CreateInstance(tipo, null);
            object[] parametersArray = new object[] { report };
            dynamic reporteResultado = tipo.GetMethod(rptReporte.NombreReporte).Invoke(classInstance, parametersArray);
            #endregion

            rptReporte.Titulo = this.ComponerTitulo(rptReporte, report);

            if (report.ReporteBase.id_TipoExport == 1)
            {
                #region [Región: Exportar Excel]
                Type tipoExcel = typeof(ExportarReporteExcel);
                object classInstanceExcel = Activator.CreateInstance(tipoExcel, null);
                object[] parametersArrayExcel = new object[] { reporteResultado, rptReporte.Titulo };
                byte[] filecontent = (byte[])tipoExcel.GetMethod(rptReporte.NombreReporte).Invoke(classInstanceExcel, parametersArrayExcel);

                stopWatch.Stop();
                tiempoTranscurrido = stopWatch.Elapsed;

                #region [Región: Logueo el movimiento en la bitacora]
                string observacionesReporte = string.Empty;
                RPT_ReporteSerializable reporteDetalleFiltro = this.ComponerMensaje(rptReporte.NombreReporte, report);

                if (reporteDetalleFiltro != null)
                {
                    reporteDetalleFiltro.NombreReporte = rptReporte.NombreReporte;
                    reporteDetalleFiltro.TituloReporte = rptReporte.Titulo;
                    observacionesReporte = ClasesHelper.Serialize(reporteDetalleFiltro);
                }

                _logBitacoraBusiness.Guardar(
                _builderBitacora.Build()
                    .With(x => x.Descripcion = "Se descargo el Excel " + rptReporte.Titulo + " (" + rptReporte.NombreReporte + "). Tiempo: " + tiempoTranscurrido.ToString(@"mm\:ss\.ff"))
                    .With(x => x.id_TipoMovimiento = 315)
                    .With(x => x.Observacion = observacionesReporte)
                    .Create());
                #endregion

                return File(filecontent, ExcelExportHelper.ExcelContentType, string.Format("{0}.xlsx", FormatoStringHelper.CleanString(rptReporte.Titulo)));
                #endregion
            }
        }

        public byte[] ReporteRegistro(List<Registro> reporte)
        {
            try
            {
                M_Excel excelNew = new M_Excel();
                M_Worksheet workSheet = new M_Worksheet();

                workSheet.Header = "Registros";
                workSheet.Data = DTHelper.ToDataTable(reporte);
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