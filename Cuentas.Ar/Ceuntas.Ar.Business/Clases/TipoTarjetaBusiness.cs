using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cuentas.Ar.Entities;
using Cuentas.Ar.Repository;

namespace Cuentas.Ar.Business
{
    public class TipoTarjetaBusiness
    {
        private readonly TipoTarjetaRepository repositorio;

        public TipoTarjetaBusiness()
        {
            this.repositorio = new TipoTarjetaRepository();
        }

        public List<TipoTarjeta> Listar()
        {
            return repositorio.Listar();
        }
    }
}