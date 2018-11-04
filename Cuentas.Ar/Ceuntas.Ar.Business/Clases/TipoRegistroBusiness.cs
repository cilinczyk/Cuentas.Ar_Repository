using System.Collections.Generic;
using Cuentas.Ar.Entities;
using Cuentas.Ar.Repository;

namespace Cuentas.Ar.Business
{
    public class TipoRegistroBusiness
    {
        private readonly TipoRegistroRepository repositorio;

        public TipoRegistroBusiness()
        {
            this.repositorio = new TipoRegistroRepository();
        }

        public List<TipoRegistro> Listar()
        {
            return repositorio.Listar();
        }
    }
}