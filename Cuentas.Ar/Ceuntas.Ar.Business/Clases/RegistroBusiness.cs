using System;
using System.Collections.Generic;
using Cuentas.Ar.Entities;
using Cuentas.Ar.Repository;

namespace Cuentas.Ar.Business
{
    public class RegistroBusiness
    {
        private readonly RegistroRepository repositorio;

        public RegistroBusiness()
        {
            this.repositorio = new RegistroRepository();
        }

        public List<Registro> Listar(M_FiltroRegistro filtroRegistro)
        {
            return repositorio.Listar(filtroRegistro);
        }

        public Registro Obtener(int idRegistro)
        {
            return repositorio.Obtener(idRegistro);
        }

        public List<Registro> ListarRegistros(int idUsuario, DateTime desde, DateTime hasta)
        {
            return repositorio.ListarRegistros(idUsuario, desde, hasta);
        }

        public decimal ObtenerAhorros(int idUsuario, int idMoneda, DateTime desde, DateTime hasta)
        {
            return repositorio.ObtenerAhorros(idUsuario, idMoneda, desde, hasta);
        }

        public Registro ObtenerCompleto(int idRegistro)
        {
            return repositorio.ObtenerCompleto(idRegistro);
        }

        public int Guardar(Registro model)
        {
            return repositorio.Guardar(model);
        }

        public int Modificar(Registro model)
        {
            return repositorio.Modificar(model);
        }

        public void Eliminar(int idRegistro)
        {
            repositorio.Eliminar(idRegistro);
        }
    }
}