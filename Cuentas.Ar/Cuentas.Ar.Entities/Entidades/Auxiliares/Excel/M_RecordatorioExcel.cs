using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cuentas.Ar.Entities
{
    public class M_RecordatorioExcel
    {
        public string EstadoRecordatorio { get; set; }
        public string Categoria { get; set; }
        public string SubCategoria { get; set; }
        public string Titulo { get; set; }
        public string Moneda { get; set; }
        public string Importe { get; set; }
        public string Descripcion { get; set; }
        public string FechaVencimiento { get; set; }
    }
}
