using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using EntityFramework.Extensions;
using LinqKit;
using Cuentas.Ar.Entities;

namespace Cuentas.Ar.Repository
{
    public class SubCategoriaRepository
    {
        public List<SubCategoria> Listar()
        {
            using (var context = new CuentasArEntities())
            {
                return context.SubCategoria.Include("Categoria").OrderBy(x => x.Descripcion).ToList();
            }
        }

        public SubCategoria Obtener(int idSubCategoria)
        {
            using (var context = new CuentasArEntities())
            {
                return context.SubCategoria.FirstOrDefault(x => x.idSubCategoria == idSubCategoria);
            }
        }

        public int Guardar(SubCategoria model)
        {
            try
            {
                using (var context = new CuentasArEntities())
                {
                    context.SubCategoria.Add(model);
                    context.SaveChanges();
                }

                return model.idCategoria;
            }
            catch (Exception ex)
            {
                throw new Exception("No se puede guardar el registro.", ex);
            }
        }

        public int Modificar(SubCategoria model)
        {
            try
            {
                using (var context = new CuentasArEntities())
                {
                    context.SubCategoria.Attach(model);
                    context.Entry(model).Property(x => x.idCategoria).IsModified = true;
                    context.Entry(model).Property(x => x.Descripcion).IsModified = true;

                    context.SaveChanges();
                }

                return model.idSubCategoria;
            }
            catch (Exception ex)
            {
                throw new Exception("No se puede modificar el registro.", ex);
            }
        }

        public void Eliminar(int idSubCategoria)
        {
            try
            {
                using (var context = new CuentasArEntities())
                {
                    context.Entry(this.Obtener(idSubCategoria)).State = EntityState.Deleted;
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