﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

	namespace Cuentas.Ar.Repository
{ 
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using Cuentas.Ar.Entities;
    
    public partial class CuentasArEntities : DbContext
    {
        public CuentasArEntities()
            : base("name=CuentasArEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Categoria> Categoria { get; set; }
        public virtual DbSet<EstadoObjetivo> EstadoObjetivo { get; set; }
        public virtual DbSet<EstadoRecordatorio> EstadoRecordatorio { get; set; }
        public virtual DbSet<Localidad> Localidad { get; set; }
        public virtual DbSet<Moneda> Moneda { get; set; }
        public virtual DbSet<Objetivo> Objetivo { get; set; }
        public virtual DbSet<Provincia> Provincia { get; set; }
        public virtual DbSet<Recordatorio> Recordatorio { get; set; }
        public virtual DbSet<Registro> Registro { get; set; }
        public virtual DbSet<SubCategoria> SubCategoria { get; set; }
        public virtual DbSet<TipoCuenta> TipoCuenta { get; set; }
        public virtual DbSet<TipoCuentaBancaria> TipoCuentaBancaria { get; set; }
        public virtual DbSet<TipoRegistro> TipoRegistro { get; set; }
        public virtual DbSet<TipoTarjeta> TipoTarjeta { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }
    }
}
