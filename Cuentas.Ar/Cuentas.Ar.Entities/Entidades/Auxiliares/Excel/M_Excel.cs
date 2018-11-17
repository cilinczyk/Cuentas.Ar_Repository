using System;
using System.Collections.Generic;
using System.Data;

namespace Cuentas.Ar.Entities
{
    public class M_Excel
    {
        public M_Excel()
        {
            this.WorksheetList = new List<M_Worksheet>();
        }

        public List<M_Worksheet> WorksheetList { get; set; }
    }

    public class M_Worksheet
    {
        public M_Worksheet()
        {
            this.Data = new DataTable();
            this.Columns = new List<M_Column>();
        }

        public DataTable Data { get; set; }

        public List<M_Column> Columns { get; set; }

        public string Header { get; set; }

        public string Name { get; set; }
    }

    public class M_Column
    {
        public M_Column(int position, string oldName, string newName, string emptyValue = "")
        {
            this.Position = position;
            this.OldName = oldName;
            this.NewName = newName;
            this.EmptyValue = emptyValue;
        }

        public M_Column(int position, string oldName, string newName, Type tipo, string emptyValue = "")
        {
            this.Position = position;
            this.OldName = oldName;
            this.NewName = newName;
            this.EmptyValue = emptyValue;
            this.Tipo = tipo;
            this.TipoDefinido = true;
        }

        public bool TipoDefinido { get; set; }

        public int Position { get; set; }

        public string OldName { get; set; }

        public string NewName { get; set; }

        public string EmptyValue { get; set; }

        public Type Tipo { get; set; }
    }
}