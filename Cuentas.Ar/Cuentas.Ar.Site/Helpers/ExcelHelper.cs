using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using Cuentas.Ar.Entities;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace Cuentas.Ar.Site.Helpers
{
    public class ExcelExportHelper
    {
        public static string ExcelContentType
        {
            get
            {
                return "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            }
        }

        public static byte[] ExportExcel(M_Excel excel, bool formateoCeldas = true)
        {
            byte[] result = null;

            using (ExcelPackage package = new ExcelPackage())
            {
                foreach (var item in excel.WorksheetList)
                {
                    ExcelWorksheet workSheet = package.Workbook.Worksheets.Add(string.Format("{0}", !string.IsNullOrEmpty(item.Name) ? item.Name : "datos"));
                    int startRowFrom = string.IsNullOrEmpty(item.Header) ? 1 : 3;

                    #region [Región: Replace/Remove Columns & Set Position]
                    List<string> listaColumnas = new List<string>();

                    DataTable clonedtable = item.Data.Clone();

                    foreach (DataColumn col in item.Data.Columns)
                    {
                        listaColumnas.Add(col.ColumnName);
                    }

                    foreach (string columna in listaColumnas)
                    {
                        M_Column column = item.Columns.FirstOrDefault(x => x.OldName == columna);
                        if (column != null && column.Tipo != null)
                        {
                            clonedtable.Columns[columna].DataType = column.Tipo;
                        }
                    }

                    foreach (DataRow dr in item.Data.Rows)
                    {
                        clonedtable.Rows.Add(dr.ItemArray);
                    }

                    foreach (string columna in listaColumnas)
                    {
                        M_Column column = item.Columns.FirstOrDefault(x => x.OldName == columna);

                        if (column != null)
                        {
                            clonedtable.Columns[columna].ColumnName = column.NewName;
                        }
                        else
                        {
                            clonedtable.Columns.RemoveAt(clonedtable.Columns.IndexOf(columna));
                        }
                    }

                    int positionTemp;
                    for (int write = 0; write < clonedtable.Columns.Count; write++)
                    {
                        for (int sort = 0; sort < clonedtable.Columns.Count - 1; sort++)
                        {
                            M_Column column = item.Columns.FirstOrDefault(x => x.NewName == clonedtable.Columns[sort].ToString());
                            M_Column column2 = item.Columns.FirstOrDefault(x => x.NewName == clonedtable.Columns[sort + 1].ToString());
                            if (column.Position > column2.Position)
                            {
                                positionTemp = sort + 1;
                                clonedtable.Columns[sort + 1].SetOrdinal(sort);
                            }
                        }
                    }
                    #endregion

                    #region [Región: Verificar Filas sin datos]
                    for (int row = 0; row < clonedtable.Rows.Count; row++)
                    {
                        for (int col = 0; col < clonedtable.Rows[row].ItemArray.Count(); col++)
                        {
                            if (string.IsNullOrEmpty(clonedtable.Rows[row].ItemArray[col].ToString()) && clonedtable.Columns[col].DataType.Name != "DateTime" && clonedtable.Columns[col].DataType.Name != "Decimal")
                            {
                                clonedtable.Rows[row].SetField(col, item.Columns.FirstOrDefault(x => x.Position == col).EmptyValue);
                            }

                            if (!string.IsNullOrEmpty(clonedtable.Rows[row].ItemArray[col].ToString()) && clonedtable.Columns[col].DataType.Name == "String")
                            {
                                clonedtable.Rows[row].SetField(col, HtmlToPlainText(clonedtable.Rows[row].ItemArray[col].ToString()));
                            }
                        }
                    }
                    #endregion

                    workSheet.Cells["A" + startRowFrom].LoadFromDataTable(clonedtable, true);

                    #region [Región: Format Header]
                    if (workSheet.Cells[startRowFrom, 1].Value != null)
                    {
                        using (ExcelRange r = workSheet.Cells[startRowFrom, 1, startRowFrom, clonedtable.Columns.Count])
                        {
                            r.Style.Font.Color.SetColor(System.Drawing.Color.White);
                            r.Style.Font.Bold = true;
                            r.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            r.Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#58819e"));
                        }
                    }
                    #endregion

                    #region [Región: Format Cells - Add Borders]
                    if (workSheet.Cells[startRowFrom + 1, 1].Value != null)
                    {
                        using (ExcelRange r = workSheet.Cells[startRowFrom + 1, 1, startRowFrom + clonedtable.Rows.Count, clonedtable.Columns.Count])
                        {
                            r.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                            r.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                            r.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                            r.Style.Border.Right.Style = ExcelBorderStyle.Thin;

                            r.Style.Border.Top.Color.SetColor(System.Drawing.Color.Black);
                            r.Style.Border.Bottom.Color.SetColor(System.Drawing.Color.Black);
                            r.Style.Border.Left.Color.SetColor(System.Drawing.Color.Black);
                            r.Style.Border.Right.Color.SetColor(System.Drawing.Color.Black);
                        }
                    }
                    #endregion

                    #region [Región: Header]
                    if (!string.IsNullOrEmpty(item.Header))
                    {
                        using (ExcelRange head = workSheet.Cells[string.Format("A1:{0}", CalcularLargoHeader(clonedtable.Columns.Count))])
                        {
                            head.Value = item.Header;
                            head.Style.Font.Size = 16;
                            head.Merge = true;
                        }
                    }
                    #endregion

                    #region [Región: Format Types Cells]
                    if (formateoCeldas)
                    {
                        if (workSheet.Cells[startRowFrom + 1, 1].Value != null)
                        {
                            for (int i = 1; i <= clonedtable.Columns.Count; i++)
                            {
                                for (int j = 1; j <= clonedtable.Rows.Count; j++)
                                {
                                    using (ExcelRange r = workSheet.Cells[startRowFrom + j, i, startRowFrom + j, i])
                                    {
                                        if (r.Value != null)
                                        {
                                            var type = r.Value.GetType();
                                            switch (type.Name)
                                            {
                                                case "DateTime":
                                                    using (ExcelRange r2 = workSheet.Cells[startRowFrom + 1, i, startRowFrom + j, i])
                                                    {
                                                        r2.Style.Numberformat.Format = "dd/MM/yyyy";
                                                    }

                                                    break;
                                                default:
                                                    break;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if (workSheet.Cells[startRowFrom + 1, 1].Value != null)
                        {
                            for (int i = 1; i <= clonedtable.Columns.Count; i++)
                            {
                                using (ExcelRange r = workSheet.Cells[startRowFrom + 1, i, startRowFrom + 1, i])
                                {
                                    if (r.Value != null)
                                    {
                                        var type = r.Value.GetType();
                                        using (ExcelRange r2 = workSheet.Cells[startRowFrom + 1, i, startRowFrom + clonedtable.Rows.Count, i])
                                        {
                                            switch (type.Name)
                                            {
                                                case "DateTime":
                                                    r2.Style.Numberformat.Format = "dd/MM/yyyy";
                                                    break;
                                                case "string":
                                                    r2.Style.Numberformat.Format = "@";
                                                    break;
                                                default:
                                                    break;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    #endregion

                    // AutoFit
                    workSheet.Cells.AutoFitColumns();
                }

                result = package.GetAsByteArray();
            }

            return result;
        }

        private static string CalcularLargoHeader(int cantColumnas)
        {
            switch (cantColumnas)
            {
                case 1:
                    {
                        return "A1";
                    }

                case 2:
                    {
                        return "B1";
                    }

                case 3:
                    {
                        return "C1";
                    }

                case 4:
                    {
                        return "D1";
                    }

                case 5:
                    {
                        return "E1";
                    }

                case 6:
                    {
                        return "F1";
                    }

                case 7:
                    {
                        return "G1";
                    }

                case 8:
                    {
                        return "H1";
                    }

                case 9:
                    {
                        return "I1";
                    }

                case 10:
                    {
                        return "J1";
                    }

                case 11:
                    {
                        return "K1";
                    }

                case 12:
                    {
                        return "L1";
                    }

                case 13:
                    {
                        return "M1";
                    }

                case 14:
                    {
                        return "N1";
                    }

                case 15:
                    {
                        return "O1";
                    }

                case 16:
                    {
                        return "P1";
                    }

                case 17:
                    {
                        return "Q1";
                    }

                case 18:
                    {
                        return "R1";
                    }

                case 19:
                    {
                        return "S1";
                    }

                case 20:
                    {
                        return "T1";
                    }

                case 21:
                    {
                        return "U1";
                    }

                case 22:
                    {
                        return "V1";
                    }

                case 23:
                    {
                        return "W1";
                    }

                case 24:
                    {
                        return "X1";
                    }

                case 25:
                    {
                        return "Y1";
                    }

                case 26:
                    {
                        return "Z1";
                    }

                case 27:
                    {
                        return "AA1";
                    }

                case 28:
                    {
                        return "AB1";
                    }

                case 29:
                    {
                        return "AC1";
                    }

                case 30:
                    {
                        return "AD1";
                    }

                case 31:
                    {
                        return "AE1";
                    }

                case 32:
                    {
                        return "AF1";
                    }

                case 33:
                    {
                        return "AG1";
                    }

                case 34:
                    {
                        return "AH1";
                    }

                case 35:
                    {
                        return "AI1";
                    }

                case 36:
                    {
                        return "AJ1";
                    }

                case 37:
                    {
                        return "AK1";
                    }

                case 38:
                    {
                        return "AL1";
                    }

                case 39:
                    {
                        return "AM1";
                    }

                case 40:
                    {
                        return "AN1";
                    }

                case 41:
                    {
                        return "AO1";
                    }

                case 42:
                    {
                        return "AP1";
                    }

                case 43:
                    {
                        return "AQ1";
                    }

                case 44:
                    {
                        return "AR1";
                    }

                case 45:
                    {
                        return "AS1";
                    }

                case 46:
                    {
                        return "AT1";
                    }

                case 47:
                    {
                        return "AU1";
                    }

                case 48:
                    {
                        return "AV1";
                    }

                case 49:
                    {
                        return "AW1";
                    }

                case 50:
                    {
                        return "AX1";
                    }

                case 51:
                    {
                        return "AY1";
                    }

                case 52:
                    {
                        return "AZ1";
                    }

                default:
                    {
                        return "E1";
                    }
            }
        }

        private static string HtmlToPlainText(string html)
        {
            const string TagWhiteSpace = @"(>|$)(\W|\n|\r)+<"; //matches one or more (white space or line breaks) between '>' and '<'
            const string StripFormatting = @"<[^>]*(>|$)"; //match any character between '<' and '>', even when end tag is missing
            const string LineBreak = @"<(br|BR)\s{0,1}\/{0,1}>"; //matches: <br>,<br/>,<br />,<BR>,<BR/>,<BR />
            var lineBreakRegex = new Regex(LineBreak, RegexOptions.Multiline);
            var stripFormattingRegex = new Regex(StripFormatting, RegexOptions.Multiline);
            var tagWhiteSpaceRegex = new Regex(TagWhiteSpace, RegexOptions.Multiline);

            var text = html;

            //Decode html specific characters
            text = System.Net.WebUtility.HtmlDecode(text);

            //Remove tag whitespace/line breaks
            text = tagWhiteSpaceRegex.Replace(text, "><");

            //Replace <br /> with line breaks
            text = lineBreakRegex.Replace(text, Environment.NewLine);

            //Strip formatting
            text = stripFormattingRegex.Replace(text, string.Empty);

            return text;
        }
    }
}