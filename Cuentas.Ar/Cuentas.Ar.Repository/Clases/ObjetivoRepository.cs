using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Cuentas.Ar.Entities;
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
                return context.Objetivo.Include("EstadoObjetivo").Include("Moneda").Where(predicado).OrderBy(x => x.Fecha).ToList();
            }
        }

        public Objetivo Obtener(int idObjetivo)
        {
            using (var context = new CuentasArEntities())
            {
                return context.Objetivo.FirstOrDefault(x => x.idObjetivo == idObjetivo);
            }
        }

        public List<Objetivo> ListarObjetivos(int idUsuario, int idMoneda)
        {
            using (var context = new CuentasArEntities())
            {
                return context.Objetivo.Where(x => x.idUsuario == idUsuario && x.idMoneda == idMoneda).ToList();
            }
        }

        public List<Objetivo> ListarObjetivos(int idUsuario, DateTime desde, DateTime hasta)
        {
            using (var context = new CuentasArEntities())
            {
                return context.Objetivo.Where(x => x.idUsuario == idUsuario && x.Fecha >= desde && x.Fecha <= hasta).ToList();
            }
        }

        public decimal ObtenerAhorros(int idUsuario, int idMoneda, DateTime desde, DateTime hasta)
        {
            using (var context = new CuentasArEntities())
            {
                return context.Objetivo.Where(x => x.idUsuario == idUsuario && x.idMoneda == idMoneda && x.Fecha >= desde && x.Fecha <= hasta).Sum(x => x.Importe);
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
                    context.Entry(model).Property(x => x.Fecha).IsModified = true;

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
                predicado = predicado.And(x => x.Fecha >= filtroObjetivo.FechaDesde.Value);
            }

            if (filtroObjetivo.FechaHasta.HasValue)
            {
                predicado = predicado.And(x => x.Fecha <= filtroObjetivo.FechaHasta.Value);
            }

            return predicado;
        }
    }
}