using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Cuentas.Ar.Entities;
using LinqKit;

namespace Cuentas.Ar.Repository
{
    public class RegistroRepository
    {
        public List<Registro> Listar(M_FiltroRegistro filtroRegistro)
        {
            using (var context = new CuentasArEntities())
            {
                var predicado = CrearPredicado(filtroRegistro);
                return context.Registro.Include("TipoRegistro").Include("Categoria").Include("SubCategoria").Include("Moneda").Where(predicado).OrderBy(x => x.Fecha).ToList();
            }
        }

        public Registro Obtener(int idRegistro)
        {
            using (var context = new CuentasArEntities())
            {
                return context.Registro.FirstOrDefault(x => x.idRegistro == idRegistro);
            }
        }

        public List<Registro> ListarRegistros(int idUsuario, DateTime desde, DateTime hasta)
        {
            using (var context = new CuentasArEntities())
            {
                return context.Registro.Where(x => x.idUsuario == idUsuario && x.Fecha >= desde && x.Fecha <= hasta).ToList();
            }
        }

        public decimal ObtenerAhorros(int idUsuario, int idMoneda, DateTime desde, DateTime hasta)
        {
            using (var context = new CuentasArEntities())
            {
                return context.Registro.Where(x => x.idUsuario == idUsuario && x.idMoneda == idMoneda && x.Fecha >= desde && x.Fecha <= hasta).Sum(x => x.Importe);
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
                    context.Entry(model).Property(x => x.Descripcion).IsModified = true;
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

        public Expression<Func<Registro, bool>> CrearPredicado(M_FiltroRegistro filtroRegistro)
        {
            var predicado = PredicateBuilder.New<Registro>(true);
            predicado = predicado.And(x => x.idUsuario == filtroRegistro.idUsuario);

            if (filtroRegistro.idTipoRegistro.HasValue)
            {
                predicado = predicado.And(x => x.idTipoRegistro == filtroRegistro.idTipoRegistro.Value);
            }

            if (filtroRegistro.idCategoria.HasValue)
            {
                predicado = predicado.And(x => x.idCategoria == filtroRegistro.idCategoria.Value);
            }

            if (filtroRegistro.idMoneda.HasValue)
            {
                predicado = predicado.And(x => x.idMoneda == filtroRegistro.idMoneda.Value);
            }

            if (filtroRegistro.FechaDesde.HasValue)
            {
                predicado = predicado.And(x => x.Fecha >= filtroRegistro.FechaDesde.Value);
            }

            if (filtroRegistro.FechaHasta.HasValue)
            {
                predicado = predicado.And(x => x.Fecha <= filtroRegistro.FechaHasta.Value);
            }

            if (filtroRegistro.Importe.HasValue)
            {
                predicado = predicado.And(x => x.Importe == filtroRegistro.Importe.Value);
            }

            return predicado;
        }
    }
}