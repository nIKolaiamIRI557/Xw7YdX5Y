// 代码生成时间: 2025-09-03 03:47:19
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using HtmlAgilityPack;

// 使用HtmlAgilityPack进行HTML解析
using HtmlDocument = HtmlAgilityPack.HtmlDocument;

namespace WebContentScraper
{
    // 网页内容抓取工具类
    public class WebContentScraper
    {
        private readonly HttpClient _httpClient;

        public WebContentScraper()
        {
            // 初始化HttpClient实例
            _httpClient = new HttpClient();
        }

        // 异步方法，用于抓取网页内容
        public async Task<string> ScrapeWebContentAsync(string url)
        {
            try
            {
                // 发送HTTP GET请求
                HttpResponseMessage response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();

                // 读取响应内容
                string content = await response.Content.ReadAsStringAsync();

                // 使用HtmlAgilityPack解析HTML
                HtmlDocument doc = new HtmlDocument();
                doc.LoadHtml(content);

                // 根据需要提取的内容，这里以提取所有链接为例
                var nodes = doc.DocumentNode.SelectNodes("//a[@href]");
                StringBuilder result = new StringBuilder();
                foreach (var node in nodes)
                {
                    result.AppendLine(node.Attributes["href"].Value);
                }

                return result.ToString();
            }
            catch (HttpRequestException e)
            {
                // 处理HTTP请求异常
                Console.WriteLine($"Error fetching web content: {e.Message}");
                return null;
            }
            catch (Exception e)
            {
                // 处理其他异常
                Console.WriteLine($"An error occurred: {e.Message}");
                return null;
            }
        }
    }

    // 程序入口类
    class Program
    {
        static async Task Main(string[] args)
        {
            WebContentScraper scraper = new WebContentScraper();
            string url = "http://example.com"; // 示例URL
            string content = await scraper.ScrapeWebContentAsync(url);
            if (content != null)
            {
                Console.WriteLine("Scraped Web Content:
");
                Console.WriteLine(content);
            }
        }
    }
}
