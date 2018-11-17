using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Cuentas.Ar.Business;
using Cuentas.Ar.Entities;

namespace Cuentas.Ar.Site.Helpers
{
    public static class ObjetivoHelper
    {
        public static void ActualizarObjetivos(int idUsuario)
        {
            var objetivoBusiness = new ObjetivoBusiness();
            var listaObjetivos = objetivoBusiness.ListarEnCurso(idUsuario);

            foreach (var item in listaObjetivos)
            {
                item.idEstadoObjetivo = ObjetivoHelper.ObtenerEstadoObjetivo(item);
                objetivoBusiness.ActualizarEstado(item);
            }
        }

        public static int ObtenerEstadoObjetivo(Objetivo objetivo)
        {
            var usuarioBusiness = new UsuarioBusiness();

            #region [Región: Estado]
            decimal capAhorro = 0;
            int mesesRestantes = (int)MonthDifference(objetivo.FechaVencimiento, DateTime.Now);

            decimal unCuarto = (objetivo.Importe * 25 / 100);
            decimal dosCuartos = (objetivo.Importe * 50 / 100);
            decimal tresCuartos = (objetivo.Importe * 75 / 100);

            if (objetivo.idMoneda == eMoneda.Pesos)
            {
                capAhorro = usuarioBusiness.Obtener(objetivo.idUsuario).CapacidadAhorroPesos * mesesRestantes;
            }
            else
            {
                capAhorro = usuarioBusiness.Obtener(objetivo.idUsuario).CapacidadAhorroDolares * mesesRestantes;
            }

            if (capAhorro >= 0 && capAhorro <= unCuarto)
            {
                return eEstadoObjetivo.Imposible;
            }
            else if (capAhorro > unCuarto && capAhorro <= dosCuartos)
            {
                return eEstadoObjetivo.Complicado;
            }
            else if (capAhorro > dosCuartos && capAhorro <= tresCuartos)
            {
                return eEstadoObjetivo.Posible;
            }
            else
            {
                return eEstadoObjetivo.Excelente;
            }
            #endregion
        }

        private static decimal MonthDifference(DateTime FechaFin, DateTime FechaInicio)
        {
            return Math.Abs((FechaFin.Month - FechaInicio.Month) + 12 * (FechaFin.Year - FechaInicio.Year));
        }
    }
}