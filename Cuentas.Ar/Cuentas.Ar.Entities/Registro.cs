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
    
    public partial class Registro
    {
        public int idRegistro { get; set; }
        public int idUsuario { get; set; }
        public int idTipoRegistro { get; set; }
        public int idCategoria { get; set; }
        public int idSubCategoria { get; set; }
        public decimal Importe { get; set; }
        public string Observaciones { get; set; }
        public System.DateTime FechaAlta { get; set; }
    
        public virtual Categoria Categoria { get; set; }
        public virtual SubCategoria SubCategoria { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}
