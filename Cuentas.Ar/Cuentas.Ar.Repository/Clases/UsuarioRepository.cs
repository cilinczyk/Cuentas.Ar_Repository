using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Cuentas.Ar.Entities;

namespace Cuentas.Ar.Repository
{
    public class UsuarioRepository
    {
        public List<Usuario> Listar()
        {
            using (var context = new CuentasArEntities())
            {
                return context.Usuario.ToList();
            }
        }

        public Usuario IniciarSesion(string email, string password)
        {
            using (var context = new CuentasArEntities())
            {
                return context.Usuario.FirstOrDefault(p => p.Email == email && p.Password == password && p.Estado == true);
            }
        }

        public bool ValidarEmail(string email)
        {
            try
            {
                using (var context = new CuentasArEntities())
                {
                    return context.Usuario.Where(p => p.Email == email).Any();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo validar el mail.", ex);
            }
            
        }

        public int Guardar(Usuario model)
        {
            try
            {
                using (var context = new CuentasArEntities())
                {
                    context.Usuario.Add(model);
                    context.SaveChanges();
                }

                return model.idUsuario;
            }
            catch (Exception ex)
            {
                throw new Exception("No se puede guardar el registro.", ex);
            }
        }

        public void Actualizar(Usuario model)
        {
            try
            {
                using (var context = new CuentasArEntities())
                {
                    context.Entry(model).State = EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("No se puede actualizar el registro.", ex);
            }
        }

        public void Eliminar(int id)
        {
            try
            {
                using (var context = new CuentasArEntities())
                {
                    var model = context.Usuario.FirstOrDefault(x => x.idUsuario == id);
                    context.Usuario.Remove(model);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("No se puede eliminar el registro.", ex);
            }
            
        }

        public Usuario Obtener(int idUsuario)
        {
            using (var context = new CuentasArEntities())
            {
                return context.Usuario.FirstOrDefault(x => x.idUsuario == idUsuario);
            }
        }

        public void Modificar(Usuario model)
        {
            try
            {
                using (var context = new CuentasArEntities())
                {
                    context.Usuario.Attach(model);
                    context.Entry(model).Property(x => x.idTipoCuenta).IsModified = true;
                    context.Entry(model).Property(x => x.idTipoTarjeta).IsModified = true;
                    context.Entry(model).Property(x => x.idProvincia).IsModified = true;
                    context.Entry(model).Property(x => x.idLocalidad).IsModified = true;
                    context.Entry(model).Property(x => x.Nombre).IsModified = true;
                    context.Entry(model).Property(x => x.FechaNacimiento).IsModified = true;
                    context.Entry(model).Property(x => x.Email).IsModified = true;
                    context.Entry(model).Property(x => x.Password).IsModified = true;
                    context.Entry(model).Property(x => x.NroTarjeta).IsModified = true;
                    context.Entry(model).Property(x => x.VencTarjeta).IsModified = true;
                    context.Entry(model).Property(x => x.CodSeguridad).IsModified = true;
                    context.Entry(model).Property(x => x.FechaCobro).IsModified = true;
                    context.Entry(model).Property(x => x.Direccion).IsModified = true;
                    context.Entry(model).Property(x => x.CodigoPostal).IsModified = true;
                    context.Entry(model).Property(x => x.Telefono).IsModified = true;
                    context.Entry(model).Property(x => x.Profesion).IsModified = true;

                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("No se puede modificar el registro.", ex);
            }
        }
    }
}