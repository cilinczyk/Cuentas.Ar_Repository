using System;
using System.ComponentModel.DataAnnotations;
using Foolproof;

namespace Cuentas.Ar.Entities
{
    [MetadataType(typeof(SubCategoriaMetadata))]
    public partial class SubCategoria
    {
    }

    public class SubCategoriaMetadata
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe seleccionar una categoría.")]
        public int idCategoria { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe ingresar una subcategoría.")]
        [StringLength(50, ErrorMessage = "La descripción debe tener un maximo de 50 caracteres.")]
        public string Descripcion { get; set; }
    }
}
