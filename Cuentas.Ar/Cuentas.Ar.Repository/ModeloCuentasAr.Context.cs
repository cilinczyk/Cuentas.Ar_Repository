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
    
        public virtual DbSet<Usuario> Usuario { get; set; }
        public virtual DbSet<TipoCuenta> TipoCuenta { get; set; }
        public virtual DbSet<TipoTarjeta> TipoTarjeta { get; set; }
    }
}
