using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cuentas.Ar.Entities
{
    public class M_FiltroObjetivo
    {
        public int idUsuario { get; set; }
        public int? idEstadoObjetivo { get; set; }
        public int? idMoneda { get; set; }
        public DateTime? FechaDesde { get; set; }
        public DateTime? FechaHasta { get; set; }
    }
}
