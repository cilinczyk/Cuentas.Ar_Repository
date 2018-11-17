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
            var reporte = registroBusiness.Listar(idUsuario, filtroReporte.FechaDesde ?? DateTime.Now.AddYears(-10), DateTime.Now.AddYears(10));
            #endregion

            #region [Región: Parsear Registro - Excel]
            List<M_RegistroExcel> listaRegistroExcel = new List<M_RegistroExcel>();
            foreach (var item in reporte)
            {
                M_RegistroExcel registroExcel = new M_RegistroExcel
                {
                    TipoRegistro = item.TipoRegistro.Descripcion,
                    Categoria = item.Categoria.Descripcion,
                    SubCategoria = item.SubCategoria.Descripcion,
                    Moneda = item.Moneda.Descripcion,
                    Importe = string.Format(new System.Globalization.CultureInfo("es-AR"), "{0:N2}", item.Importe),
                    Fecha = item.Fecha.ToShortDateString(),
                    Descripcion = !string.IsNullOrEmpty(item.Descripcion) ? item.Descripcion : "-"
                };

                listaRegistroExcel.Add(registroExcel);
            }
            #endregion

            #region [Región: Exportar Excel]
            byte[] filecontent = ReporteRegistro(listaRegistroExcel);

            return File(filecontent, ExcelExportHelper.ExcelContentType, string.Format("{0}.xlsx", "MisCuentasReporte_Registros"));
            #endregion
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public FileContentResult Objetivo(M_FiltroReporte filtroReporte, int idTipoReporte = 1)
        {
            #region [Región: Obtener Reporte]
            var objetivoBusiness = new ObjetivoBusiness();

            int idUsuario = Convert.ToInt32(ClaimsPrincipal.Current.FindFirst(ClaimTypes.Sid).Value);
            var reporte = objetivoBusiness.Listar(idUsuario, filtroReporte.FechaDesde ?? DateTime.Now.AddYears(-10), DateTime.Now.AddYears(10));
            #endregion

            #region [Región: Parsear Registro - Excel]
            List<M_ObjetivoExcel> listaObjetivoExcel = new List<M_ObjetivoExcel>();
            foreach (var item in reporte)
            {
                M_ObjetivoExcel objetivoExcel = new M_ObjetivoExcel
                {
                    Motivo = item.Motivo,
                    EstadoObjetivo = item.EstadoObjetivo.Descripcion,
                    Moneda = item.Moneda.Descripcion,
                    Importe = string.Format(new System.Globalization.CultureInfo("es-AR"), "{0:N2}", item.Importe),
                    FechaVencimiento = item.FechaVencimiento.ToShortDateString(),
                    Descripcion = !string.IsNullOrEmpty(item.Descripcion) ? item.Descripcion : "-"
                };

                listaObjetivoExcel.Add(objetivoExcel);
            }
            #endregion

            #region [Región: Exportar Excel]
            byte[] filecontent = ReporteObjetivo(listaObjetivoExcel);

            return File(filecontent, ExcelExportHelper.ExcelContentType, string.Format("{0}.xlsx", "MisCuentasReporte_Objetivos"));
            #endregion
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public FileContentResult Recordatorio(M_FiltroReporte filtroReporte, int idTipoReporte = 1)
        {
            #region [Región: Obtener Reporte]
            var recordatorioBusiness = new RecordatorioBusiness();

            int idUsuario = Convert.ToInt32(ClaimsPrincipal.Current.FindFirst(ClaimTypes.Sid).Value);
            var reporte = recordatorioBusiness.Listar(idUsuario, filtroReporte.FechaDesde ?? DateTime.Now.AddYears(-10), DateTime.Now.AddYears(10));
            #endregion

            #region [Región: Parsear Registro - Excel]
            List<M_RecordatorioExcel> listaRecordatorioExcel = new List<M_RecordatorioExcel>();
            foreach (var item in reporte)
            {
                M_RecordatorioExcel recordatorioExcel = new M_RecordatorioExcel
                {
                    EstadoRecordatorio = item.EstadoRecordatorio.Descripcion,
                    Categoria = item.Categoria.Descripcion,
                    SubCategoria = item.SubCategoria.Descripcion,
                    Titulo = item.Titulo,
                    Moneda = item.Moneda.Descripcion,
                    Importe = string.Format(new System.Globalization.CultureInfo("es-AR"), "{0:N2}", item.Importe),
                    FechaVencimiento = item.FechaVencimiento.ToShortDateString(),
                    Descripcion = !string.IsNullOrEmpty(item.Descripcion) ? item.Descripcion : "-"
                };

                listaRecordatorioExcel.Add(recordatorioExcel);
            }
            #endregion

            #region [Región: Exportar Excel]
            byte[] filecontent = ReporteRecordatorio(listaRecordatorioExcel);

            return File(filecontent, ExcelExportHelper.ExcelContentType, string.Format("{0}.xlsx", "MisCuentasReporte_Recordatorios"));
            #endregion
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

                workSheet.Columns.Add(new M_Column(0, "TipoRegistro", "Tipo de Registro"));
                workSheet.Columns.Add(new M_Column(1, "Categoria", "Categoría"));
                workSheet.Columns.Add(new M_Column(2, "SubCategoria", "SubCategoría"));
                workSheet.Columns.Add(new M_Column(3, "Moneda", "Moneda"));
                workSheet.Columns.Add(new M_Column(4, "Importe", "Importe"));
                workSheet.Columns.Add(new M_Column(5, "Fecha", "Fecha", typeof(DateTime)));
                workSheet.Columns.Add(new M_Column(6, "Descripcion", "Descripción"));

                excelNew.WorksheetList.Add(workSheet);

                return ExcelExportHelper.ExportExcel(excelNew);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private byte[] ReporteObjetivo(List<M_ObjetivoExcel> reporte)
        {
            try
            {
                M_Excel excelNew = new M_Excel();
                M_Worksheet workSheet = new M_Worksheet
                {
                    Header = "Reporte de Objetivos",
                    Data = DTHelper.ToDataTable(reporte),
                };

                workSheet.Columns.Add(new M_Column(0, "Motivo", "Motivo"));
                workSheet.Columns.Add(new M_Column(1, "EstadoObjetivo", "Estado"));
                workSheet.Columns.Add(new M_Column(4, "Moneda", "Moneda"));
                workSheet.Columns.Add(new M_Column(5, "Importe", "Importe"));
                workSheet.Columns.Add(new M_Column(6, "FechaVencimiento", "Fecha de Vencimiento", typeof(DateTime)));
                workSheet.Columns.Add(new M_Column(7, "Descripcion", "Descripción"));

                excelNew.WorksheetList.Add(workSheet);

                return ExcelExportHelper.ExportExcel(excelNew);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private byte[] ReporteRecordatorio(List<M_RecordatorioExcel> reporte)
        {
            try
            {
                M_Excel excelNew = new M_Excel();
                M_Worksheet workSheet = new M_Worksheet
                {
                    Header = "Reporte de Recordatorios",
                    Data = DTHelper.ToDataTable(reporte),
                };

                workSheet.Columns.Add(new M_Column(0, "Titulo", "Titulo"));
                workSheet.Columns.Add(new M_Column(1, "EstadoRecordatorio", "Estado"));
                workSheet.Columns.Add(new M_Column(2, "Categoria", "Categoría"));
                workSheet.Columns.Add(new M_Column(3, "SubCategoria", "SubCategoría"));
                workSheet.Columns.Add(new M_Column(4, "Moneda", "Moneda"));
                workSheet.Columns.Add(new M_Column(5, "Importe", "Importe"));
                workSheet.Columns.Add(new M_Column(6, "Fecha", "Fecha de Vencimiento", typeof(DateTime)));
                workSheet.Columns.Add(new M_Column(7, "Descripcion", "Descripción"));

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