using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cuentas.Ar.Entities
{
    public class M_FiltroRegistro
    {
        public int idTipoRegistro { get; set; }
        public int idCategoria { get; set; }
        public int idSubCategoria { get; set; }
        public int idMoneda { get; set; }
        public DateTime FechaDesde { get; set; }
        public DateTime FechaHasta { get; set; }
        public decimal Importe { get; set; }
        public string Descripcion { get; set; }
    }
}
