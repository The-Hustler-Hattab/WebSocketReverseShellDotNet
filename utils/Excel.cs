using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WebSocketReverseShellDotNet.utils
{
    internal class Excel
    {
        public static FileInfo ConvertToExcel(Dictionary<string, List<object>> dataDictionary, string filePath)
        {
            if (dataDictionary == null || !dataDictionary.Any())
            {
                Console.WriteLine("No data to export.");
                return null;
            }

            using (var package = new ExcelPackage())
            {
                foreach (var kvp in dataDictionary)
                {
                    var worksheet = package.Workbook.Worksheets.Add(kvp.Key);

                    // Headers
                    if (kvp.Value.Any())
                    {
                        PropertyInfo[] properties = kvp.Value.First().GetType().GetProperties();
                        for (int i = 0; i < properties.Length; i++)
                        {
                            worksheet.Cells[1, i + 1].Value = properties[i].Name;
                        }

                        // Data
                        for (int row = 0; row < kvp.Value.Count; row++)
                        {
                            for (int col = 0; col < properties.Length; col++)
                            {
                                worksheet.Cells[row + 2, col + 1].Value = properties[col].GetValue(kvp.Value[row]);
                            }
                        }
                    }
                }

                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }

                // Save the Excel file
                FileInfo excelFile = new FileInfo(filePath);
                package.SaveAs(excelFile);

                Console.WriteLine($"Excel file saved to: {filePath}");
                return excelFile;
            }
        }
    }
}
