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
            M_FiltroMisCuentas filtroMisCuentas = new M_FiltroMisCuentas();
            filtroMisCuentas.FechaDesde = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            filtroMisCuentas.FechaHasta = filtroMisCuentas.FechaDesde.AddMonths(1).AddDays(-1);

            M_Home model = CompletarDatosHome(idUsuario, filtroMisCuentas);

            #region [Región: Actualizar Estados Objetivo
            var objetivoBusiness = new ObjetivoBusiness();
            objetivoBusiness.ActualizarEstados(idUsuario);
            #endregion

            Session["FiltroMisCuentas"] = model.FiltroMisCuentas;
            return View(model);
        }

        private static M_Home CompletarDatosHome(int idUsuario, M_FiltroMisCuentas filtroMisCuentas)
        {
            Usuario usuario = new UsuarioBusiness().Obtener(idUsuario);
            M_Home model = new M_Home();

            decimal ingresos = 0;
            decimal gastos = 0;
            decimal netoPesos = 0;
            decimal netoDolares = 0;
            decimal ahorrosPesos = 0;
            decimal ahorrosDolares = 0;

            ingresos = usuario?.Registro.Where(x => x.idTipoRegistro == eTipoRegistro.Ingreso && x.idMoneda == eMoneda.Pesos && x.idCategoria != eCategoria.Ahorros)?.Sum(x => x.Importe) ?? 0;
            gastos = usuario?.Registro.Where(x => x.idTipoRegistro == eTipoRegistro.Gasto && x.idMoneda == eMoneda.Pesos && x.idCategoria != eCategoria.Ahorros)?.Sum(x => x.Importe) ?? 0;
            netoPesos = ingresos - gastos;

            ingresos = usuario?.Registro.Where(x => x.idTipoRegistro == eTipoRegistro.Ingreso && x.idMoneda == eMoneda.Dolares && x.idCategoria != eCategoria.Ahorros)?.Sum(x => x.Importe) ?? 0;
            gastos = usuario?.Registro.Where(x => x.idTipoRegistro == eTipoRegistro.Gasto && x.idMoneda == eMoneda.Dolares && x.idCategoria != eCategoria.Ahorros)?.Sum(x => x.Importe) ?? 0;
            netoDolares = ingresos - gastos;

            ingresos = usuario?.Registro.Where(x => x.idTipoRegistro == eTipoRegistro.Ingreso && x.idMoneda == eMoneda.Pesos && x.idCategoria == eCategoria.Ahorros)?.Sum(x => x.Importe) ?? 0;
            gastos = usuario?.Registro.Where(x => x.idTipoRegistro == eTipoRegistro.Gasto && x.idMoneda == eMoneda.Pesos && x.idCategoria == eCategoria.Ahorros)?.Sum(x => x.Importe) ?? 0;
            ahorrosPesos = ingresos - gastos;

            ingresos = usuario?.Registro.Where(x => x.idTipoRegistro == eTipoRegistro.Ingreso && x.idMoneda == eMoneda.Dolares && x.idCategoria == eCategoria.Ahorros)?.Sum(x => x.Importe) ?? 0;
            gastos = usuario?.Registro.Where(x => x.idTipoRegistro == eTipoRegistro.Gasto && x.idMoneda == eMoneda.Dolares && x.idCategoria == eCategoria.Ahorros)?.Sum(x => x.Importe) ?? 0;
            ahorrosDolares = ingresos - gastos;

            model = new M_Home();
            model.FiltroMisCuentas.FechaDesde = filtroMisCuentas.FechaDesde;
            model.FiltroMisCuentas.FechaHasta = filtroMisCuentas.FechaHasta;

            model.MisCuentas.Usuario = usuario.Nombre;
            model.MisCuentas.FechaDesde = filtroMisCuentas.FechaDesde;
            model.MisCuentas.FechaHasta = filtroMisCuentas.FechaHasta;
            model.MisCuentas.SaldoPesos = string.Format(new System.Globalization.CultureInfo("es-AR"), "{0:N2}", netoPesos);
            model.MisCuentas.SaldoDolares = string.Format(new System.Globalization.CultureInfo("es-AR"), "{0:N2}", netoDolares);
            model.MisCuentas.AhorrosPesos = string.Format(new System.Globalization.CultureInfo("es-AR"), "{0:N2}", ahorrosPesos);
            model.MisCuentas.AhorrosDolares = string.Format(new System.Globalization.CultureInfo("es-AR"), "{0:N2}", ahorrosDolares);

            return model;
        }

        #region [Región: Búsqueda]
        public ActionResult Buscar(M_FiltroMisCuentas filtroMisCuentas)
        {
            Session["FiltroMisCuentas"] = filtroMisCuentas;
            return RedirectToAction("MisCuentas", "Home");
        }

        public ActionResult MisCuentas()
        {
            int idUsuario = Convert.ToInt32(ClaimsPrincipal.Current.FindFirst(ClaimTypes.Sid).Value);
            M_FiltroMisCuentas filtroMisCuentas = Session["FiltroMisCuentas"] as M_FiltroMisCuentas;
            M_Home model = CompletarDatosHome(idUsuario, filtroMisCuentas);

            return PartialView("_MisCuentas", model.MisCuentas);
        }
        #endregion

        [HttpPost]
        public JsonResult RefrescarGraficoEstadoActual()
        {
            try
            {
                var registroBusiness = new RegistroBusiness();
                int idUsuario = Convert.ToInt32(ClaimsPrincipal.Current.FindFirst(ClaimTypes.Sid).Value);

                #region [Región: Declaraciones]
                decimal ingresos = 0;
                decimal gastos = 0;
                decimal saldoPesos = 0;
                decimal saldoDolares = 0;
                decimal ahorrosPesos = 0;
                decimal ahorrosDolares = 0;

                bool estadoGrafico = false;
                List<string> data = new List<string>();
                List<string> labels = new List<string>();
                #endregion

                #region [Región: Labels]
                labels.Add("Saldo (Pesos)");
                labels.Add("Saldo (Dolares)");
                labels.Add("Ahorros (Pesos)");
                labels.Add("Ahorros (Dolares)");
                #endregion

                #region [Región: Data]
                var listaRegistros = registroBusiness.Listar(idUsuario);

                ingresos = listaRegistros.Where(x => x.idTipoRegistro == eTipoRegistro.Ingreso && x.idMoneda == eMoneda.Pesos).Sum(x => x.Importe);
                gastos = listaRegistros.Where(x => x.idTipoRegistro == eTipoRegistro.Gasto && x.idMoneda == eMoneda.Pesos).Sum(x => x.Importe);
                saldoPesos = ingresos - gastos;

                ingresos = listaRegistros.Where(x => x.idTipoRegistro == eTipoRegistro.Ingreso && x.idMoneda == eMoneda.Dolares).Sum(x => x.Importe);
                gastos = listaRegistros.Where(x => x.idTipoRegistro == eTipoRegistro.Gasto && x.idMoneda == eMoneda.Dolares).Sum(x => x.Importe);
                saldoDolares = ingresos - gastos;

                ahorrosPesos = listaRegistros.Where(x => x.idCategoria == eCategoria.Ahorros && x.idMoneda == eMoneda.Pesos).Sum(x => x.Importe);
                ahorrosDolares = listaRegistros.Where(x => x.idCategoria == eCategoria.Ahorros && x.idMoneda == eMoneda.Dolares).Sum(x => x.Importe);

                data.Add(saldoPesos.ToString().Replace(',','.'));
                data.Add(saldoDolares.ToString().Replace(',', '.'));
                data.Add(ahorrosPesos.ToString().Replace(',', '.'));
                data.Add(ahorrosDolares.ToString().Replace(',', '.'));

                estadoGrafico = (saldoPesos + saldoDolares + ahorrosPesos + ahorrosDolares) != 0 ? true : false;
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

        [HttpPost]
        public JsonResult RefrescarGraficoFlujoDinero()
        {
            try
            {
                var registroBusiness = new RegistroBusiness();
                int idUsuario = Convert.ToInt32(ClaimsPrincipal.Current.FindFirst(ClaimTypes.Sid).Value);

                #region [Región: Declaraciones]
                decimal ingresos = 0;
                decimal gastos = 0;
                decimal ahorros = 0;

                bool estadoGrafico = false;
                List<string> data = new List<string>();
                List<string> labels = new List<string>();
                #endregion

                #region [Región: Labels]
                labels.Add("Ingresos");
                labels.Add("Gastos");
                labels.Add("Ahorros");
                #endregion

                #region [Región: Data]
                var fechaDesde = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                var fechaHasta = fechaDesde.AddMonths(1).AddDays(-1);

                var listaRegistros = registroBusiness.Listar(idUsuario, fechaDesde, fechaHasta);
                ingresos = listaRegistros.Where(x => x.idTipoRegistro == eTipoRegistro.Ingreso && x.idCategoria != eCategoria.Ahorros).Sum(x => x.Importe);
                gastos = listaRegistros.Where(x => x.idTipoRegistro == eTipoRegistro.Gasto).Sum(x => x.Importe);
                ahorros = listaRegistros.Where(x => x.idCategoria == eCategoria.Ahorros).Sum(x => x.Importe);

                data.Add(ingresos.ToString().Replace(',', '.'));
                data.Add(gastos.ToString().Replace(',', '.'));
                data.Add(ahorros.ToString().Replace(',', '.'));

                estadoGrafico = (ingresos + gastos + ahorros) != 0 ? true : false;
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

        [HttpPost]
        public JsonResult RefrescarGraficoBalance()
        {
            try
            {
                var registroBusiness = new RegistroBusiness();
                int idUsuario = Convert.ToInt32(ClaimsPrincipal.Current.FindFirst(ClaimTypes.Sid).Value);

                #region [Región: Declaraciones]
                decimal ingresos = 0;
                decimal gastos = 0;
                decimal neto = 0;

                bool estadoGrafico = false;
                List<string> data = new List<string>();
                List<string> labels = new List<string>();
                #endregion

                
                #region [Región: Data]
                var fechaDesde = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                var fechaHasta = fechaDesde.AddMonths(1).AddDays(-1);

                var listaRegistros = registroBusiness.Listar(idUsuario, fechaDesde, fechaHasta);

                #region [Región: Labels y Date]
                List<string> listaDias = listaRegistros.Select(x => x.Fecha.ToString("dd/MM")).Distinct().ToList();

                foreach (var diaMes in listaDias)
                {
                    labels.Add(diaMes);

                    ingresos = listaRegistros.Where(x => x.idTipoRegistro == eTipoRegistro.Ingreso && x.Fecha.ToString("dd/MM") == diaMes).Sum(x => x.Importe);
                    gastos = listaRegistros.Where(x => x.idTipoRegistro == eTipoRegistro.Gasto && x.Fecha.ToString("dd/MM") == diaMes).Sum(x => x.Importe);
                    neto = ingresos - gastos;

                    data.Add(neto.ToString().Replace(',', '.'));
                }

                estadoGrafico = listaRegistros.Count > 0 ? true : false;
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

        [HttpPost]
        public JsonResult RefrescarGraficoGastosCategoria()
        {
            try
            {
                var registroBusiness = new RegistroBusiness();
                int idUsuario = Convert.ToInt32(ClaimsPrincipal.Current.FindFirst(ClaimTypes.Sid).Value);

                #region [Región: Declaraciones]
                decimal gastosCategoria = 0;

                bool estadoGrafico = false;
                List<string> data = new List<string>();
                List<string> labels = new List<string>();
                #endregion


                #region [Región: Data]
                var fechaDesde = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                var fechaHasta = fechaDesde.AddMonths(1).AddDays(-1);

                var listaRegistros = registroBusiness.Listar(idUsuario, fechaDesde, fechaHasta).Where(x => x.idCategoria != eCategoria.Ahorros);

                #region [Región: Labels y Data]
                Dictionary<int, string> listaCategoriasPesos = new Dictionary<int, string>();
                Dictionary<int, string> listaCategoriasDolares = new Dictionary<int, string>();

                //Gastos en Pesos
                foreach (var item in listaRegistros)
                {
                    if (item.idMoneda == eMoneda.Pesos)
                    {
                        if (!listaCategoriasPesos.Any(x => x.Key == item.idCategoria))
                        {
                            listaCategoriasPesos.Add(item.idCategoria, item.Categoria.Descripcion);
                        }
                    }
                    else
                    {
                        if (!listaCategoriasDolares.Any(x => x.Key == item.idCategoria))
                        {
                            listaCategoriasDolares.Add(item.idCategoria, item.Categoria.Descripcion);
                        }
                    }
                }

                foreach (var categoria in listaCategoriasPesos)
                {
                    labels.Add(string.Format("{0} ARS", categoria.Value));

                    gastosCategoria = listaRegistros.Where(x => x.idCategoria == categoria.Key).Sum(x => x.Importe);
                    data.Add(gastosCategoria.ToString().Replace(',', '.'));
                }

                foreach (var categoria in listaCategoriasDolares)
                {
                    labels.Add(string.Format("{0} USD", categoria.Value));

                    gastosCategoria = listaRegistros.Where(x => x.idCategoria == categoria.Key).Sum(x => x.Importe);
                    data.Add(gastosCategoria.ToString().Replace(',', '.'));
                }

                estadoGrafico = listaRegistros.Count() > 0 ? true : false;
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