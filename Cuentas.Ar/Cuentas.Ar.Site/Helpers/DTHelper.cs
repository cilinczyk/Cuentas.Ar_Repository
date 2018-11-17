using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;

namespace Cuentas.Ar.Site.Helpers
{
    public static class DTHelper
    {
        public static IEnumerable<PropertyInfo> PropInfos { get; set; }

        public static List<T> ConvertToList<T>(this DataTable table) where T : new()
        {
            Type t = typeof(T);

            List<T> returnObject = new List<T>();

            foreach (DataRow dr in table.Rows)
            {
                T newRow = ConvertToEntity<T>(dr);
                returnObject.Add(newRow);
            }

            return returnObject;
        }

        public static T ConvertToEntity<T>(this DataRow tableRow) where T : new()
        {
            Type t = typeof(T);
            T returnObject = new T();

            foreach (DataColumn col in tableRow.Table.Columns)
            {
                string colName = col.ColumnName;

                PropertyInfo propInfo = t.GetProperty(colName.ToLower(), BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

                if (propInfo != null)
                {
                    object val = tableRow[colName];

                    bool isNullable = Nullable.GetUnderlyingType(propInfo.PropertyType) != null;
                    if (isNullable)
                    {
                        if (val is System.DBNull)
                        {
                            val = null;
                        }
                        else
                        {
                            val = Convert.ChangeType(val, Nullable.GetUnderlyingType(propInfo.PropertyType));
                        }
                    }
                    else
                    {
                        val = Convert.ChangeType(val, propInfo.PropertyType);
                    }

                    propInfo.SetValue(returnObject, val, null);
                }
            }

            return returnObject;
        }

        public static DataTable ConvertToDataTable(this object obj)
        {
            PropertyInfo[] propInfos = obj.GetType().GetProperties();

            var table = new DataTable();

            foreach (PropertyInfo propInfo in propInfos)
            {
                table.Columns.Add(propInfo.Name, propInfo.GetType());
            }

            DataRow row = table.NewRow();

            foreach (PropertyInfo propInfo in propInfos)
            {
                row[propInfo.Name] = propInfo.GetValue(obj, null);
            }

            return table;
        }

        public static DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);

            PropertyInfo[] props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (PropertyInfo prop in props)
            {
                dataTable.Columns.Add(prop.Name);
            }

            foreach (T item in items)
            {
                var values = new object[props.Length];
                for (int i = 0; i < props.Length; i++)
                {
                    values[i] = props[i].GetValue(item, null);
                }

                dataTable.Rows.Add(values);
            }

            return dataTable;
        }

        public static bool ComprobarFilaVacia(DataRow dr)
        {
            if (dr == null)
            {
                return true;
            }
            else
            {
                foreach (var value in dr.ItemArray)
                {
                    if (!string.IsNullOrEmpty(value.ToString()))
                    {
                        return false;
                    }
                }

                return true;
            }
        }
    }
}