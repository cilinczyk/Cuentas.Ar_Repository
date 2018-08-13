using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cuentas.Ar.Entities
{
    public class M_Usuario
    {
        //[Required(AllowEmptyStrings = false, ErrorMessageResourceName = "DAT_USU_LOGIN", ErrorMessageResourceType = typeof(Resources.i18n))]
        //[EmailAddress(ErrorMessageResourceName = "DAT_GEN_CORREOVAL", ErrorMessageResourceType = typeof(Resources.i18n), ErrorMessage = null)]
        //[DataType(DataType.EmailAddress, ErrorMessageResourceName = "DAT_GEN_CORREOVAL", ErrorMessageResourceType = typeof(Resources.i18n), ErrorMessage = null)]
        //[StringLength(255, ErrorMessageResourceType = typeof(Resources.i18n), ErrorMessageResourceName = "DAT_GEN_MAXLENGTH")]
        public string Mail { get; set; }

        //[Required(AllowEmptyStrings = false, ErrorMessageResourceName = "DAT_USU_PASS", ErrorMessageResourceType = typeof(Resources.i18n))]
        //[StringLength(10, ErrorMessageResourceName = "DAT_USU_PASSLENGTH", ErrorMessageResourceType = typeof(Resources.i18n), MinimumLength = 6)]
        //[DataType(DataType.Password)]
        public string Password { get; set; }

        public bool Recordarme { get; set; }
    }
}
