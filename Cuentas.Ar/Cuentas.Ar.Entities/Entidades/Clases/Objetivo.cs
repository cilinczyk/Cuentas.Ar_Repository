using System;
using System.ComponentModel.DataAnnotations;
using Foolproof;

namespace Cuentas.Ar.Entities
{
    [MetadataType(typeof(ObjetivoMetadata))]
    public partial class Objetivo
    {
    }

    public class ObjetivoMetadata
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe seleccionar un estado.")]
        public int idEstadoObjetivo { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe seleccionar una moneda.")]
        public int idMoneda { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe ingresar un titulo.")]
        public string Motivo { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe ingresar un importe.")]
        public decimal Importe { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe ingresar una fecha.")]
        public System.DateTime FechaVencimiento { get; set; }
    }
}
