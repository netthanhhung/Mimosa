using System;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Browser;
using System.Windows;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Windows.Controls;

namespace Mimosa.Apartment.Silverlight.UI
{
    public static class ReportHelper
    {                
        public static void BuildStringOfRow(StringBuilder strBuilder, List<string> lstFields, string strFormat)
        {
            switch (strFormat)
            {
                case "XML":
                    strBuilder.AppendLine("<Row>");
                    strBuilder.AppendLine(String.Join("\r\n", lstFields.ToArray()));
                    strBuilder.AppendLine("</Row>");
                    break;
                case "CSV":
                    strBuilder.AppendLine(String.Join(",", lstFields.ToArray()));
                    break;
            }
        }

        public static string FormatField(string data, string strFormat)
        {
            switch (strFormat)
            {
                case "XML":
                    return String.Format("<Cell><Data ss:Type=\"String" + "\">{0}</Data></Cell>", data);
                case "CSV":
                    return String.Format("\"{0}\"", data.Replace("\"", "\"\"\"").Replace("\n", "").Replace("\r", ""));
            }
            return data;
        }

        public static void AddEmptyColumn(List<string> lstFields, int numberOfColumns, string strFormat)
        {
            for (int i = 0; i < numberOfColumns; i++)
            {
                lstFields.Add(FormatField(string.Empty, strFormat));
            }
        }

        public static void ExportStringToFile(SaveFileDialog objSFD, string reportName, string strFormat, StringBuilder strBuilder)
        {
            StreamWriter sw = new StreamWriter(objSFD.OpenFile());
            if (strFormat == "XML")
            {
                //Let us write the headers for the Excel XML
                sw.WriteLine("<?xml version=\"1.0\" " +
                             "encoding=\"utf-8\"?>");
                sw.WriteLine("<?mso-application progid" +
                             "=\"Excel.Sheet\"?>");
                sw.WriteLine("<Workbook xmlns=\"urn:" +
                             "schemas-microsoft-com:office:spreadsheet\">");
                sw.WriteLine("<DocumentProperties " +
                             "xmlns=\"urn:schemas-microsoft-com:" +
                             "office:office\">");
                sw.WriteLine("<Author>Synergi IT Team</Author>");
                sw.WriteLine("<Created>" +
                             DateTime.Now.ToLocalTime().ToLongDateString() +
                             "</Created>");
                sw.WriteLine("<LastSaved>" +
                             DateTime.Now.ToLocalTime().ToLongDateString() +
                             "</LastSaved>");
                sw.WriteLine("<ReportName>" + reportName + "</ReportName>");
                sw.WriteLine("</DocumentProperties>");
                sw.WriteLine("<Worksheet ss:Name=\"" + reportName + "\" " +
                   "xmlns:ss=\"urn:schemas-microsoft-com:office:spreadsheet\">");
                sw.WriteLine("<Table>");
            }
            sw.Write(strBuilder.ToString());
            if (strFormat == "XML")
            {
                sw.WriteLine("</Table>");
                sw.WriteLine("</Worksheet>");
                sw.WriteLine("</Workbook>");
            }
            sw.Close();
        }

        public static string FormatValueColumn(decimal? value, string fortmat, string strFormat)
        {
            string result = "0";
            if (value.HasValue)
            {
                result = ReportHelper.FormatField(value.Value.ToString(fortmat), strFormat);
            }
            return result;
        }

    }
}
