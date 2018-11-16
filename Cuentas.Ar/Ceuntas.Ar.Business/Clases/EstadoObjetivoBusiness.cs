using System.Collections.Generic;
using Cuentas.Ar.Entities;
using Cuentas.Ar.Repository;

namespace Cuentas.Ar.Business
{
    public class EstadoObjetivoBusiness
    {
        private readonly EstadoObjetivoRepository repositorio;

        public EstadoObjetivoBusiness()
        {
            this.repositorio = new EstadoObjetivoRepository();
        }

        public List<EstadoObjetivo> Listar()
        {
            return repositorio.Listar();
        }
    }
}