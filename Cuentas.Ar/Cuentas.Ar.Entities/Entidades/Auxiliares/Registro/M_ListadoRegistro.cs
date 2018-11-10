using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cuentas.Ar.Entities
{
    public class M_ListadoRegistro
    {
        public M_ListadoRegistro()
        {
            this.FiltroRegistro = new M_FiltroRegistro();
            this.ListaRegistro = new List<Registro>();
        }

        public M_FiltroRegistro FiltroRegistro { get; set; }
        public List<Registro> ListaRegistro { get; set; }
    }
}