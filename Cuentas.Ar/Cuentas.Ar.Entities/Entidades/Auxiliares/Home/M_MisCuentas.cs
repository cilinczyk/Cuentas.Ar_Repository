﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cuentas.Ar.Entities
{
    public class M_MisCuentas
    {
        public M_MisCuentas()
        {
            this.ListaUltimosRecordatorios = new List<Recordatorio>();
        }

        public string Usuario { get; set; }

        public DateTime FechaDesde { get; set; }

        public DateTime FechaHasta { get; set; }

        public string TipoCuenta { get; set; }

        public string SaldoPesos { get; set; }

        public string SaldoDolares { get; set; }

        public string AhorrosPesos { get; set; }

        public string AhorrosDolares { get; set; }

        public string SaldoActualPesos { get; set; }

        public string SaldoActualDolares { get; set; }

        public string AhorrosActualPesos { get; set; }

        public string AhorrosActualDolares { get; set; }

        public List<Recordatorio> ListaUltimosRecordatorios { get; set; }
    }
}
