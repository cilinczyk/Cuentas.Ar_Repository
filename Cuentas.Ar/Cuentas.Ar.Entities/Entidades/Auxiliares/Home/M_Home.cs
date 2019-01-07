using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cuentas.Ar.Entities
{
    public class M_Home
    {
        public M_Home()
        {
            this.FiltroMisCuentas = new M_FiltroMisCuentas();
            this.MisCuentas = new M_MisCuentas();
        }

        public M_FiltroMisCuentas FiltroMisCuentas { get; set; }
        public M_MisCuentas MisCuentas { get; set; }
    }
}
