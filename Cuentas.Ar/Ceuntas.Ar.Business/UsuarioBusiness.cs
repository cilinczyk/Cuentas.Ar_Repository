using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cuentas.Ar.Entities;
using Cuentas.Ar.Repository;

namespace Cuentas.Ar.Business
{
    public class UsuarioBusiness
    {
        private readonly UsuarioRepository repositorio;

        public UsuarioBusiness()
        {
            this.repositorio = new UsuarioRepository();
        }

        public List<Usuario> Listar()
        {
            return repositorio.Listar();
        }

        public bool ValidarEmail(string email)
        {
            return repositorio.ValidarEmail(email);
        }

        public Usuario IniciarSesion(string email, string password)
        {
            return null;
        }
    }
}