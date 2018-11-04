using System.Collections.Generic;
using Cuentas.Ar.Entities;
using Cuentas.Ar.Repository;

namespace Cuentas.Ar.Business
{
    public class TipoCuentaBusiness
    {
        private readonly TipoCuentaRepository repositorio;

        public TipoCuentaBusiness()
        {
            this.repositorio = new TipoCuentaRepository();
        }

        public List<TipoCuenta> Listar()
        {
            return repositorio.Listar();
        }
    }
}