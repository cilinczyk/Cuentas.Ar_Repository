using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cuentas.Ar.Entities;

namespace Cuentas.Ar.Repository
{
    public class TipoTarjetaRepository
    {
        public List<TipoTarjeta> Listar()
        {
            using (var context = new CuentasArEntities())
            {
                return context.TipoTarjeta.ToList();
            }
        }
    }
}