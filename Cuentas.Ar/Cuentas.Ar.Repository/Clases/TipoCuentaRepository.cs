using System.Collections.Generic;
using System.Linq;
using Cuentas.Ar.Entities;

namespace Cuentas.Ar.Repository
{
    public class TipoCuentaRepository
    {
        public List<TipoCuenta> Listar()
        {
            using (var context = new CuentasArEntities())
            {
                return context.TipoCuenta.Where(x => x.idTipoCuenta != eTipoCuenta.Free).ToList();
            }
        }
    }
}