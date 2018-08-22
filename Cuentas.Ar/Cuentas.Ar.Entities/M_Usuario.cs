using System.ComponentModel.DataAnnotations;

namespace Cuentas.Ar.Entities
{
    public class M_Usuario
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe ingresar un mail.")]
        [EmailAddress(ErrorMessage = "Debe ingresar un correo electrónico válido.")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Debe ingresar un correo electrónico válido.")]
        [StringLength(255, ErrorMessage ="Debe ingresar un mail con un máximo de 255 caracteres.")]
        public string Mail { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe ingresar una contraseña.")]
        [StringLength(10, ErrorMessage = "La contraseña debe tener entre 6 y 10 caracteres.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool Recordarme { get; set; }
    }
}
