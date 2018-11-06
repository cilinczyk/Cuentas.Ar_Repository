using System;
using System.ComponentModel.DataAnnotations;
using Foolproof;

namespace Cuentas.Ar.Entities
{
    [MetadataType(typeof(UsuarioMetadata))]
    public partial class Usuario
    {
        public bool UsuarioActualizado { get; set; }
    }

    public class UsuarioMetadata
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe ingresar un nombre.")]
        [StringLength(50, ErrorMessage = "El nombre debe tener un maximo de 50 caracteres.")]
        public string Nombre { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<DateTime> FechaNacimiento { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe ingresar un email.")]
        [EmailAddress(ErrorMessage = "Debe ingresar un email válido.")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Debe ingresar un email válido.")]
        [StringLength(255, ErrorMessage = "Debe ingresar un email con un máximo de 255 caracteres.")]
        public string Email { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe ingresar una contraseña.")]
        [StringLength(40, ErrorMessage = "La password debe tener un maximo de 40 caracteres.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe ingresar un tipo de cuenta.")]
        public int idTipoCuenta { get; set; }

        [RequiredIfNot("idTipoCuenta", eTipoCuenta.Free, ErrorMessage = "Debe seleccionar una tarjeta de credito.")]
        public byte? idTipoTarjeta { get; set; }

        [RequiredIfNot("idTipoCuenta", eTipoCuenta.Free, ErrorMessage = "Debe ingresar un número de tarjeta.")]
        [StringLength(16, ErrorMessage = "El número de tarjeta debe contener 16 dígitos.")]
        [RegularExpression("([0-9]+)", ErrorMessage = "Debe ingresar un número de tarjeta valido.")]
        public string NroTarjeta { get; set; }

        [RequiredIfNot("idTipoCuenta", eTipoCuenta.Free, ErrorMessage = "Debe ingresar un código de seguridad.")]
        [RegularExpression("^[0-9]{3,4}$", ErrorMessage = "Debe ingresar un codigo de seguridad valido.")]
        public string CodSeguridad { get; set; }

        [RequiredIfNot("idTipoCuenta", eTipoCuenta.Free, ErrorMessage = "Debe ingresar un vencimiento de tarjeta.")]
        [RegularExpression(@"^\d{1,2}\/\d{1,2}$", ErrorMessage = "Debe ingresar un vencimiento valido.")]
        public string VencTarjeta { get; set; }

        public bool Estado { get; set; }
        public Nullable<bool> Sexo { get; set; }
    }
}
