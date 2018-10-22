using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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