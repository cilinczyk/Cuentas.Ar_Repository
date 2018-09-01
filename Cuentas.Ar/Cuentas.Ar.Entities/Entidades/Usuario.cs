using System;
using System.ComponentModel.DataAnnotations;

namespace Cuentas.Ar.Entities
{
    [MetadataType(typeof(UsuarioMetadata))]
    public partial class Usuario
    {
        public int idTipoCuenta { get; set; }
    }

    public class UsuarioMetadata
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe ingresar un nombre.")]
        [StringLength(50, ErrorMessage = "El nombre debe tener un maximo de 50 caracteres.")]
        public string Nombre { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
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

        public bool Administrador { get; set; }
        public bool Estado { get; set; }
        public Nullable<bool> Sexo { get; set; }
    }
}
