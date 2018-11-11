//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Cuentas.Ar.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class Usuario
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Usuario()
        {
            this.Categoria = new HashSet<Categoria>();
            this.Registro = new HashSet<Registro>();
            this.SubCategoria = new HashSet<SubCategoria>();
            this.Recordatorio = new HashSet<Recordatorio>();
        }
    
        public int idUsuario { get; set; }
        public int idTipoCuenta { get; set; }
        public Nullable<int> idTipoTarjeta { get; set; }
        public Nullable<int> idProvincia { get; set; }
        public Nullable<int> idLocalidad { get; set; }
        public string Nombre { get; set; }
        public Nullable<System.DateTime> FechaNacimiento { get; set; }
        public Nullable<bool> Sexo { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string NroTarjeta { get; set; }
        public string VencTarjeta { get; set; }
        public string CodSeguridad { get; set; }
        public Nullable<System.DateTime> FechaCobro { get; set; }
        public string Direccion { get; set; }
        public string CodigoPostal { get; set; }
        public string Telefono { get; set; }
        public string Profesion { get; set; }
        public System.DateTime FechaAlta { get; set; }
        public bool Estado { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Categoria> Categoria { get; set; }
        public virtual Localidad Localidad { get; set; }
        public virtual Provincia Provincia { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Registro> Registro { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SubCategoria> SubCategoria { get; set; }
        public virtual TipoCuenta TipoCuenta { get; set; }
        public virtual TipoTarjeta TipoTarjeta { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Recordatorio> Recordatorio { get; set; }
    }
}
