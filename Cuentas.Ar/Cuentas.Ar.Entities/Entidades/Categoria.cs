using System;
using System.ComponentModel.DataAnnotations;
using Foolproof;

namespace Cuentas.Ar.Entities
{
    [MetadataType(typeof(CategoriaMetadata))]
    public partial class Categoria
    {
    }

    public class CategoriaMetadata
    {

        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe seleccionar un tipo de registro.")]
        public int idTipoRegistro { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe ingresar una categoría.")]
        [StringLength(50, ErrorMessage = "La categoría no debe tener un maximo de 50 caracteres.")]
        public string Descripcion { get; set; }
    }
}
