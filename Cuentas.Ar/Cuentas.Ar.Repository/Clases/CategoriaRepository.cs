using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cuentas.Ar.Entities;

namespace Cuentas.Ar.Repository
{
    public class CategoriaRepository
    {
        public List<Categoria> Listar()
        {
            using (var context = new CuentasArEntities())
            {
                return context.Categoria.Include("TipoRegistro").OrderBy(x => x.Descripcion).ToList();
            }
        }
    }
}