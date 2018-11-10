using System.Collections.Generic;
using System.Linq;
using Cuentas.Ar.Entities;

namespace Cuentas.Ar.Repository
{
    public class MonedaRepository
    {
        public List<Moneda> Listar()
        {
            using (var context = new CuentasArEntities())
            {
                return context.Moneda.ToList();
            }
        }
    }
}