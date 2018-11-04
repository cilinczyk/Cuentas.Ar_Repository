using System.Collections.Generic;
using System.Linq;
using Cuentas.Ar.Entities;

namespace Cuentas.Ar.Repository
{
    public class ProvinciaRepository
    {
        public List<Provincia> Listar()
        {
            using (var context = new CuentasArEntities())
            {
                return context.Provincia.ToList();
            }
        }
    }
}