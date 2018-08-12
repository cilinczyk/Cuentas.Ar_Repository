using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cuentas.Ar.Entities;

namespace Cuentas.Ar.Repository
{
    public class UsuarioRepository
    {
        public List<Usuario> Listar()
        {
            return null;
        }

        public Usuario IniciarSesion(string email, string password)
        {
            using (var context = new CuentasArEntities())
            {
                return context.Usuario.Include("GEN_Idioma").FirstOrDefault(p => p.Email == email && p.Password == password && p.Estado == true);
            }
        }
    }
}
