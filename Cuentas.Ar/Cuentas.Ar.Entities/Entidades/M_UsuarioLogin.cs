using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Cuentas.Ar.Entities.Validaciones;
using Foolproof;

namespace Cuentas.Ar.Entities
{
    public class M_UsuarioLogin
    {
        public M_UsuarioLogin()
        {
            DatosBasicos = new M_DatosBasicos();
            FormaPago = new M_FormaPago();
        }

        public int idTipoCuenta { get; set; }
        public M_DatosBasicos DatosBasicos { get; set; }
        public M_FormaPago FormaPago { get; set; }
    }

    public class M_DatosBasicos
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Ingrese un nombre.")]
        [StringLength(50, ErrorMessage = "El nombre debe tener un máximo de 50 caracteres.")]
        public string Nombre { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Ingrese un email.")]
        [EmailAddress(ErrorMessage = "Ingrese un email válido.")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Ingrese un email valido.")]
        [StringLength(50, ErrorMessage = "Ingrese un email con un máximo de 50 caracteres.")]
        public string Email { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Ingrese una contraseña.")]
        [StringLength(8, ErrorMessage = "Ingrese uan contraseña con un maximo de 8 caracteres.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }

    public class M_FormaPago
    {
        [Required(ErrorMessage = "Seleccione una tarjeta de credito.")]
        public byte? idTipoTarjeta { get; set; }

        [Required(ErrorMessage = "Ingrese un número de tarjeta.")]
        [StringLength(16, ErrorMessage = "El nro. de tarjeta debe contener 16 dígitos.")]
        [RegularExpression("([0-9]+)", ErrorMessage = "Ingrese un número de tarjeta valido.")]
        [CheckCreditCard]
        public string NroTarjeta { get; set; }

        [Required(ErrorMessage = "Ingrese un código de seguridad.")]
        [RegularExpression("^[0-9]{3,4}$", ErrorMessage = "Ingrese un código de seguridad valido.")]
        public string CodSeguridad { get; set; }

        [Required(ErrorMessage = "Ingrese un vencimiento de tarjeta.")]
        [RegularExpression(@"^\d{1,2}\/\d{1,2}$", ErrorMessage = "Ingrese un vencimiento valido.")]
        public string VencTarjeta { get; set; }
    }
}
