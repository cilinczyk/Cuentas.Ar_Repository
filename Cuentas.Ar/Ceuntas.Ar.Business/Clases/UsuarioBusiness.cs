using System.Collections.Generic;
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
            return repositorio.IniciarSesion(email, password);
        }

        public int Guardar (Usuario model)
        {
            return repositorio.Guardar(model);
        }

        public Usuario Obtener(int idUsuario)
        {
            return repositorio.Obtener(idUsuario);
        }
    }
}