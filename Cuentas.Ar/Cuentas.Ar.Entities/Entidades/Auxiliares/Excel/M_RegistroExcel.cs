﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cuentas.Ar.Entities
{
    public class M_RegistroExcel
    {
        public string TipoRegistro { get; set; }
        public string Categoria { get; set; }
        public string SubCategoria { get; set; }
        public string Moneda { get; set; }
        public string Importe { get; set; }
        public string Fecha { get; set; }
        public string Descripcion { get; set; }
    }
}
