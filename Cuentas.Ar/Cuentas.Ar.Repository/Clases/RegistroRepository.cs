using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Cuentas.Ar.Entities;

namespace Cuentas.Ar.Repository
{
    public class RegistroRepository
    {
        public List<Registro> Listar()
        {
            using (var context = new CuentasArEntities())
            {
                return context.Registro.Include("TipoRegistro").Include("Categoria").Include("SubCategoria").Include("Moneda").OrderBy(x => x.Fecha).ToList();
            }
        }

        public Registro Obtener(int idRegistro)
        {
            using (var context = new CuentasArEntities())
            {
                return context.Registro.FirstOrDefault(x => x.idRegistro == idRegistro);
            }
        }

        public Registro ObtenerCompleto(int idRegistro)
        {
            using (var context = new CuentasArEntities())
            {
                return context.Registro.Include("TipoRegistro").Include("Categoria").Include("SubCategoria").Include("Moneda").FirstOrDefault(x => x.idRegistro == idRegistro);
            }
        }

        public int Guardar(Registro model)
        {
            try
            {
                using (var context = new CuentasArEntities())
                {
                    context.Registro.Add(model);
                    context.SaveChanges();
                }

                return model.idRegistro;
            }
            catch (Exception ex)
            {
                throw new Exception("No se puede guardar el registro.", ex);
            }
        }

        public int Modificar(Registro model)
        {
            try
            {
                using (var context = new CuentasArEntities())
                {
                    context.Registro.Attach(model);
                    context.Entry(model).Property(x => x.idTipoRegistro).IsModified = true;
                    context.Entry(model).Property(x => x.idCategoria).IsModified = true;
                    context.Entry(model).Property(x => x.idSubCategoria).IsModified = true;
                    context.Entry(model).Property(x => x.idMoneda).IsModified = true;
                    context.Entry(model).Property(x => x.Importe).IsModified = true;
                    context.Entry(model).Property(x => x.Observaciones).IsModified = true;
                    context.Entry(model).Property(x => x.Fecha).IsModified = true;

                    context.SaveChanges();
                }

                return model.idRegistro;
            }
            catch (Exception ex)
            {
                throw new Exception("No se puede modificar el registro.", ex);
            }
        }

        public void Eliminar(int idRegistro)
        {
            try
            {
                using (var context = new CuentasArEntities())
                {
                    context.Entry(this.Obtener(idRegistro)).State = EntityState.Deleted;
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