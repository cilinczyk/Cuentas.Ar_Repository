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

        [StringLength(50, ErrorMessage = "La categoría no debe tener un maximo de 50 caracteres.")]
        public string Descripcion { get; set; }
    }
}
