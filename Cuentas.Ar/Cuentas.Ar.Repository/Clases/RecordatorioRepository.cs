using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Cuentas.Ar.Entities;

namespace Cuentas.Ar.Repository
{
    public class RecordatorioRepository
    {
        public List<Recordatorio> Listar()
        {
            using (var context = new CuentasArEntities())
            {
                return context.Recordatorio.Include("Categoria").Include("EstadoRecordatorio").Include("Moneda").OrderBy(x => x.Titulo).ToList();
            }
        }

        public List<Recordatorio> Listar(int idUsuario, DateTime desde, DateTime hasta)
        {
            using (var context = new CuentasArEntities())
            {
                return context.Recordatorio.Include("Categoria").Include("SubCategoria").Include("EstadoRecordatorio").Include("Moneda").Where(x => x.idUsuario == idUsuario && x.FechaVencimiento >= desde && x.FechaVencimiento <= hasta).ToList();
            }
        }

        public List<Recordatorio> ListarUltimos(int idUsuario, int cantidad)
        {
            using (var context = new CuentasArEntities())
            {
                return context.Recordatorio.Include("EstadoRecordatorio").Where(x => x.idUsuario == idUsuario).Take(cantidad).OrderByDescending(x => x.FechaVencimiento).ToList();
            }
        }

        public List<Recordatorio> ListarUltimos(int idUsuario, int cantidad, DateTime fechaDesde, DateTime fechaHasta)
        {
            using (var context = new CuentasArEntities())
            {
                return context.Recordatorio.Include("EstadoRecordatorio").Where(x => x.idUsuario == idUsuario && x.FechaVencimiento >= desde && x.FechaVencimiento <= hasta).Take(cantidad).OrderByDescending(x => x.FechaVencimiento).ToList();
            }
        }

        public Recordatorio Obtener(int idRecordatorio)
        {
            using (var context = new CuentasArEntities())
            {
                return context.Recordatorio.FirstOrDefault(x => x.idRecordatorio == idRecordatorio);
            }
        }

        public Recordatorio ObtenerCompleto(int idRecordatorio)
        {
            using (var context = new CuentasArEntities())
            {
                return context.Recordatorio.Include("Categoria").Include("SubCategoria").Include("EstadoRecordatorio").Include("Moneda").FirstOrDefault(x => x.idRecordatorio == idRecordatorio);
            }
        }

        public int Guardar(Recordatorio model)
        {
            try
            {
                using (var context = new CuentasArEntities())
                {
                    context.Recordatorio.Add(model);
                    context.SaveChanges();
                }

                return model.idRecordatorio;
            }
            catch (Exception ex)
            {
                throw new Exception("No se puede guardar el registro.", ex);
            }
        }

        public int Modificar(Recordatorio model)
        {
            try
            {
                using (var context = new CuentasArEntities())
                {
                    context.Recordatorio.Attach(model);
                    context.Entry(model).Property(x => x.idEstado).IsModified = true;
                    context.Entry(model).Property(x => x.idCategoria).IsModified = true;
                    context.Entry(model).Property(x => x.idSubCategoria).IsModified = true;
                    context.Entry(model).Property(x => x.idMoneda).IsModified = true;
                    context.Entry(model).Property(x => x.Importe).IsModified = true;
                    context.Entry(model).Property(x => x.Titulo).IsModified = true;
                    context.Entry(model).Property(x => x.Descripcion).IsModified = true;
                    context.Entry(model).Property(x => x.FechaVencimiento).IsModified = true;

                    context.SaveChanges();
                }

                return model.idRecordatorio;
            }
            catch (Exception ex)
            {
                throw new Exception("No se puede modificar el registro.", ex);
            }
        }

        public void Eliminar(int idRecordatorio)
        {
            try
            {
                using (var context = new CuentasArEntities())
                {
                    context.Entry(this.Obtener(idRecordatorio)).State = EntityState.Deleted;
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