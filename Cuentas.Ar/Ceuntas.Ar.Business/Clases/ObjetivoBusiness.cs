using System;
using System.Collections.Generic;
using Cuentas.Ar.Entities;
using Cuentas.Ar.Repository;

namespace Cuentas.Ar.Business
{
    public class ObjetivoBusiness
    {
        private readonly ObjetivoRepository repositorio;

        public ObjetivoBusiness()
        {
            this.repositorio = new ObjetivoRepository();
        }

        public List<Objetivo> Listar(M_FiltroObjetivo filtroObjetivo)
        {
            return repositorio.Listar(filtroObjetivo);
        }

        public Objetivo Obtener(int idObjetivo)
        {
            return repositorio.Obtener(idObjetivo);
        }

        public List<Objetivo> ListarObjetivos(int idUsuario, int idMoneda)
        {
            return repositorio.ListarObjetivos(idUsuario, idMoneda);
        }

        public List<Objetivo> ListarObjetivos(int idUsuario, DateTime desde, DateTime hasta)
        {
            return repositorio.ListarObjetivos(idUsuario, desde, hasta);
        }

        public Objetivo ObtenerCompleto(int idObjetivo)
        {
            return repositorio.ObtenerCompleto(idObjetivo);
        }

        public int Guardar(Objetivo model)
        {
            return repositorio.Guardar(model);
        }

        public int Modificar(Objetivo model)
        {
            return repositorio.Modificar(model);
        }

        public void Eliminar(int idObjetivo)
        {
            repositorio.Eliminar(idObjetivo);
        }

        public void ActualizarEstados(int idUsuario)
        {
            repositorio.ActualizarEstados(idUsuario);
        }
    }
}