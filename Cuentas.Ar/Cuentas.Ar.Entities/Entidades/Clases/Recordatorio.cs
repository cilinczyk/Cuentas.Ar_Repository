using System;
using System.ComponentModel.DataAnnotations;
using Foolproof;

namespace Cuentas.Ar.Entities
{
    [MetadataType(typeof(RecordatorioMetadata))]
    public partial class Recordatorio
    {
    }

    public class RecordatorioMetadata
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe seleccionar un estado.")]
        public int idEstado { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe seleccionar una categoría.")]
        public int idCategoria { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe ingresar un titulo.")]
        public string Titulo { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe ingresar una fecha.")]
        public System.DateTime FechaVencimiento { get; set; }
    }
}
