using System.Collections.Generic;
using System.Linq;
using Cuentas.Ar.Entities;

namespace Cuentas.Ar.Repository
{
    public class LocalidadRepository
    {
        public List<Localidad> Listar(int idProvincia)
        {
            using (var context = new CuentasArEntities())
            {
                return context.Localidad.Where(x => x.idProvincia == idProvincia).ToList();
            }
        }
    }
}