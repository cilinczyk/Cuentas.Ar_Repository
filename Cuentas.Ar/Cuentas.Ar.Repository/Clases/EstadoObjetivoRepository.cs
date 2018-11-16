using System.Collections.Generic;
using System.Linq;
using Cuentas.Ar.Entities;

namespace Cuentas.Ar.Repository
{
    public class EstadoObjetivoRepository
    {
        public List<EstadoObjetivo> Listar()
        {
            using (var context = new CuentasArEntities())
            {
                return context.EstadoObjetivo.ToList();
            }
        }
    }
}