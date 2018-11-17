using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Cuentas.Ar.Entities;

namespace Cuentas.Ar.Repository
{
    public class CategoriaRepository
    {
        public List<Categoria> Listar(int idUsuario)
        {
            using (var context = new CuentasArEntities())
            {
                return context.Categoria.Include("TipoRegistro").Where(x => x.idUsuario == null || x.idUsuario == idUsuario).OrderBy(x => x.Descripcion).ToList();
            }
        }

        public List<Categoria> Listar(int idUsuario, int idTipoRegistro)
        {
            using (var context = new CuentasArEntities())
            {
                return context.Categoria.Where(x => x.idTipoRegistro == idTipoRegistro && (x.idUsuario == null || x.idUsuario == idUsuario)).OrderBy(x => x.Descripcion).ToList();
            }
        }

        public Categoria Obtener(int idCategoria)
        {
            using (var context = new CuentasArEntities())
            {
                return context.Categoria.FirstOrDefault(x => x.idCategoria == idCategoria);
            }
        }

        public int Guardar(Categoria model)
        {
            try
            {
                using (var context = new CuentasArEntities())
                {
                    context.Categoria.Add(model);
                    context.SaveChanges();
                }

                return model.idCategoria;
            }
            catch (Exception ex)
            {
                throw new Exception("No se puede guardar el registro.", ex);
            }
        }

        public int Modificar(Categoria model)
        {
            try
            {
                using (var context = new CuentasArEntities())
                {
                    context.Categoria.Attach(model);
                    context.Entry(model).Property(x => x.idTipoRegistro).IsModified = true;
                    context.Entry(model).Property(x => x.Descripcion).IsModified = true;

                    context.SaveChanges();
                }

                return model.idCategoria;
            }
            catch (Exception ex)
            {
                throw new Exception("No se puede modificar el registro.", ex);
            }
        }

        public void Eliminar(int idCategoria)
        {
            try
            {
                using (var context = new CuentasArEntities())
                {
                    context.Entry(this.Obtener(idCategoria)).State = EntityState.Deleted;
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("No se puede eliminar el registro.", ex);
            }
        }
    }
}