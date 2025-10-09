using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Core.Extension
{
   public class ExcelXmlExporter
    {
       
            public static byte[] ExportToExcelXml<T>(IEnumerable<T> data, string sheetName = "Sheet1")
            {
                var props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
                var sb = new StringBuilder();

                var settings = new XmlWriterSettings
                {
                    Encoding = Encoding.UTF8,
                    Indent = true
                };

                using (var stringWriter = new StringWriter(sb))
                using (var writer = XmlWriter.Create(stringWriter, settings))
                {
                    writer.WriteStartDocument();
                    writer.WriteStartElement("Workbook", "urn:schemas-microsoft-com:office:spreadsheet");
                    writer.WriteAttributeString("xmlns", "ss", null, "urn:schemas-microsoft-com:office:spreadsheet");

                    writer.WriteStartElement("Worksheet");
                    writer.WriteAttributeString("ss", "Name", null, sheetName);
                    writer.WriteStartElement("Table");

                    // Başlık satırı
                    writer.WriteStartElement("Row");
                    foreach (var prop in props)
                    {
                        writer.WriteStartElement("Cell");
                        writer.WriteStartElement("Data");
                        writer.WriteAttributeString("ss", "Type", null, "String");
                        writer.WriteString(prop.Name);
                        writer.WriteEndElement(); // Data
                        writer.WriteEndElement(); // Cell
                    }
                    writer.WriteEndElement(); // Row

                    // Veriler
                    foreach (var item in data)
                    {
                        writer.WriteStartElement("Row");
                        foreach (var prop in props)
                        {
                            var value = prop.GetValue(item)?.ToString() ?? "";
                            writer.WriteStartElement("Cell");
                            writer.WriteStartElement("Data");
                            writer.WriteAttributeString("ss", "Type", null, "String");
                            writer.WriteString(value);
                            writer.WriteEndElement(); // Data
                            writer.WriteEndElement(); // Cell
                        }
                        writer.WriteEndElement(); // Row
                    }

                    writer.WriteEndElement(); // Table
                    writer.WriteEndElement(); // Worksheet
                    writer.WriteEndElement(); // Workbook
                }

                return Encoding.UTF8.GetBytes(sb.ToString());
            }
        }
    
}
