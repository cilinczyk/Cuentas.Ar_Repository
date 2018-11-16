using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cuentas.Ar.Entities
{
    public class M_ListadoObjetivo
    {
        public M_ListadoObjetivo()
        {
            this.FiltroObjetivo = new M_FiltroObjetivo();
            this.ListaObjetivo = new List<Objetivo>();
        }

        public M_FiltroObjetivo FiltroObjetivo { get; set; }
        public List<Objetivo> ListaObjetivo { get; set; }
    }
}