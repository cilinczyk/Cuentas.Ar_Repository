using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Cuentas.Ar.Entities;
using EntityFramework.Extensions;
using LinqKit;

namespace Cuentas.Ar.Repository
{
    public class ObjetivoRepository
    {
        public List<Objetivo> Listar(M_FiltroObjetivo filtroObjetivo)
        {
            using (var context = new CuentasArEntities())
            {
                var predicado = CrearPredicado(filtroObjetivo);
                return context.Objetivo.Include("EstadoObjetivo").Include("Moneda").Where(predicado).OrderBy(x => x.FechaVencimiento).ToList();
            }
        }

        public List<Objetivo> Listar(int idUsuario, DateTime desde, DateTime hasta)
        {
            using (var context = new CuentasArEntities())
            {
                return context.Objetivo.Include("EstadoObjetivo").Include("Moneda").Where(x => x.idUsuario == idUsuario && x.FechaVencimiento >= desde && x.FechaVencimiento <= hasta).ToList();
            }
        }

        public List<Objetivo> ListarEnCurso(int idUsuario)
        {
            using (var context = new CuentasArEntities())
            {
                return context.Objetivo.Where(x => x.idUsuario == idUsuario && x.idEstadoObjetivo != eEstadoObjetivo.Finalizado).ToList();
            }
        }

        public Objetivo Obtener(int idObjetivo)
        {
            using (var context = new CuentasArEntities())
            {
                return context.Objetivo.FirstOrDefault(x => x.idObjetivo == idObjetivo);
            }
        }

        public decimal ObtenerAhorros(int idUsuario, int idMoneda, DateTime desde, DateTime hasta)
        {
            using (var context = new CuentasArEntities())
            {
                return context.Objetivo.Where(x => x.idUsuario == idUsuario && x.idMoneda == idMoneda && x.FechaVencimiento >= desde && x.FechaVencimiento <= hasta).Sum(x => x.Importe);
            }
        }

        public Objetivo ObtenerCompleto(int idObjetivo)
        {
            using (var context = new CuentasArEntities())
            {
                return context.Objetivo.Include("EstadoObjetivo").Include("Moneda").FirstOrDefault(x => x.idObjetivo == idObjetivo);
            }
        }

        public int Guardar(Objetivo model)
        {
            try
            {
                using (var context = new CuentasArEntities())
                {
                    context.Objetivo.Add(model);
                    context.SaveChanges();
                }

                return model.idObjetivo;
            }
            catch (Exception ex)
            {
                throw new Exception("No se puede guardar el registro.", ex);
            }
        }

        public int Modificar(Objetivo model)
        {
            try
            {
                using (var context = new CuentasArEntities())
                {
                    context.Objetivo.Attach(model);
                    context.Entry(model).Property(x => x.idEstadoObjetivo).IsModified = true;
                    context.Entry(model).Property(x => x.idMoneda).IsModified = true;
                    context.Entry(model).Property(x => x.Importe).IsModified = true;
                    context.Entry(model).Property(x => x.Motivo).IsModified = true;
                    context.Entry(model).Property(x => x.Descripcion).IsModified = true;
                    context.Entry(model).Property(x => x.FechaVencimiento).IsModified = true;

                    context.SaveChanges();
                }

                return model.idObjetivo;
            }
            catch (Exception ex)
            {
                throw new Exception("No se puede modificar el registro.", ex);
            }
        }

        public void Eliminar(int idObjetivo)
        {
            try
            {
                using (var context = new CuentasArEntities())
                {
                    context.Entry(this.Obtener(idObjetivo)).State = EntityState.Deleted;
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("No se puede eliminar el registro.", ex);
            }
        }

        public Expression<Func<Objetivo, bool>> CrearPredicado(M_FiltroObjetivo filtroObjetivo)
        {
            var predicado = PredicateBuilder.New<Objetivo>(true);
            predicado = predicado.And(x => x.idUsuario == filtroObjetivo.idUsuario);

            if (filtroObjetivo.idEstadoObjetivo.HasValue)
            {
                predicado = predicado.And(x => x.idEstadoObjetivo == filtroObjetivo.idEstadoObjetivo.Value);
            }

            if (filtroObjetivo.idMoneda.HasValue)
            {
                predicado = predicado.And(x => x.idMoneda == filtroObjetivo.idMoneda.Value);
            }

            if (filtroObjetivo.FechaDesde.HasValue)
            {
                predicado = predicado.And(x => x.FechaVencimiento >= filtroObjetivo.FechaDesde.Value);
            }

            if (filtroObjetivo.FechaHasta.HasValue)
            {
                predicado = predicado.And(x => x.FechaVencimiento <= filtroObjetivo.FechaHasta.Value);
            }

            return predicado;
        }

        public void ActualizarEstados(int idUsuario)
        {
            try
            {
                using (var context = new CuentasArEntities())
                {
                    context.Objetivo.Where(x => x.idUsuario == idUsuario && x.FechaVencimiento < DateTime.Now && x.idEstadoObjetivo != eEstadoObjetivo.Finalizado).Update(x => new Objetivo() { idEstadoObjetivo = eEstadoObjetivo.Finalizado });
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("No se puede actualizar el registro.", ex);
            }
        }

        public void ActualizarEstado(Objetivo model)
        {
            try
            {
                using (var context = new CuentasArEntities())
                {
                    context.Objetivo.Where(x => x.idObjetivo == model.idObjetivo).Update(x => new Objetivo() { idEstadoObjetivo = model.idEstadoObjetivo });
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("No se puede actualizar el registro.", ex);
            }
        }
    }
}