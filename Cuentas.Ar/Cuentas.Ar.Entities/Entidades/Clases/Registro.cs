using System;
using System.ComponentModel.DataAnnotations;
using Foolproof;

namespace Cuentas.Ar.Entities
{
    [MetadataType(typeof(RegistroMetadata))]
    public partial class Registro
    {
    }

    public class RegistroMetadata
    {

        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe seleccionar un tipo de registro.")]
        public int idTipoRegistro { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe seleccionar una categoría.")]
        public int idCategoria { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe seleccionar una moneda.")]
        public int idMoneda { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe ingresar un importe.")]
        public decimal Importe { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe ingresar una fecha.")]
        public System.DateTime Fecha { get; set; }

        [StringLength(50, ErrorMessage = "La descripción debe tener un maximo de 50 caracteres.")]
        public string Descripcion { get; set; }
    }
}
