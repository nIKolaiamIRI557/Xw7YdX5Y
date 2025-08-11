// 代码生成时间: 2025-08-12 06:02:45
// TestReportGenerator.cs
// 这个类用于生成测试报告

using System;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace TestReportGenerator
{
    public class TestReportGenerator
    {
        private readonly DbContext _context;

        public TestReportGenerator(DbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// 生成测试报告
        /// </summary>
        /// <param name="reportTitle">报告标题</param>
        /// <param name="data">测试数据</param>
        /// <returns>生成的报告文件路径</returns>
        public string GenerateReport(string reportTitle, IQueryable<object> data)
        {
            try
            {
                // 创建报告文件路径
                string reportFilePath = Path.Combine(Directory.GetCurrentDirectory(), "TestReports", $"{reportTitle}.html");

                // 确保目录存在
                Directory.CreateDirectory(Path.GetDirectoryName(reportFilePath));

                // 写入HTML头部
                string header = "<html><head><title>" + reportTitle + "</title></head><body>
                    <h1>" + reportTitle + "</h1>
";

                // 将数据转换为HTML表格形式
                string htmlContent = ConvertDataToHtml(data);

                // 写入HTML尾部
                string footer = "</body></html>";

                // 将HTML内容写入文件
                File.WriteAllText(reportFilePath, header + htmlContent + footer);

                return reportFilePath;
            }
            catch (Exception ex)
            {
                // 错误处理
                Console.WriteLine("Error generating report: " + ex.Message);
                return null;
            }
        }

        /// <summary>
        /// 将测试数据转换为HTML表格形式
        /// </summary>
        /// <param name="data">测试数据</param>
        /// <returns>HTML表格字符串</returns>
        private string ConvertDataToHtml(IQueryable<object> data)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<table border='1'><tr>");

            // 获取对象的属性名称作为表头
            var properties = data.AsEnumerable().SelectMany(obj => obj.GetType().GetProperties().Select(p => p.Name)).ToList();
            foreach (var property in properties)
            {
                sb.AppendFormat("<th>{0}</th>", property);
            }
            sb.Append("</tr>");

            // 遍历数据并将每个对象的属性值作为表格的一行
            foreach (var item in data)
            {
                sb.Append("<tr>");
                foreach (var property in properties)
                {
                    var value = item.GetType().GetProperty(property).GetValue(item, null);
                    sb.AppendFormat("<td>{0}</td>", value ?? "null");
                }
                sb.Append("</tr>");
            }

            sb.Append("</table>");
            return sb.ToString();
        }
    }
}
