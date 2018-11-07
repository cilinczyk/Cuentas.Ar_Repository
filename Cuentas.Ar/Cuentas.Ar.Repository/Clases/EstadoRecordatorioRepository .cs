using System.Collections.Generic;
using System.Linq;
using Cuentas.Ar.Entities;

namespace Cuentas.Ar.Repository
{
    public class EstadoRecordatorioRepository
    {
        public List<EstadoRecordatorio> Listar()
        {
            using (var context = new CuentasArEntities())
            {
                return context.EstadoRecordatorio.ToList();
            }
        }
    }
}