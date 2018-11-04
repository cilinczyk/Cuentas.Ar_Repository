using System.Collections.Generic;
using System.Linq;
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