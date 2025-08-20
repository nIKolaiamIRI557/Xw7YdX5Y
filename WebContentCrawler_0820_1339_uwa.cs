// 代码生成时间: 2025-08-20 13:39:24
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using HtmlAgilityPack;

namespace WebContentCrawler
{
    /// <summary>
    /// A simple web content crawler that fetches and extracts content from web pages.
    /// </summary>
    public class WebContentCrawler
    {
        private readonly HttpClient _httpClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="WebContentCrawler"/> class.
        /// </summary>
        public WebContentCrawler()
        {
            _httpClient = new HttpClient();
        }

        /// <summary>
        /// Fetches and extracts content from a given URL.
        /// </summary>
        /// <param name="url">The URL of the webpage to fetch.</param>
        /// <returns>A string representing the content of the webpage.</returns>
        public async Task<string> FetchWebContentAsync(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                throw new ArgumentException("URL cannot be null or empty.", nameof(url));
            }

            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();
                string htmlContent = await response.Content.ReadAsStringAsync();

                HtmlDocument htmlDoc = new HtmlDocument();
                htmlDoc.LoadHtml(htmlContent);

                // Regular expression to remove script and style elements
                var scriptAndStyleRegex = new Regex("<(script|style).*?>.*?<(\/script|\/style)>",
                     RegexOptions.Singleline | RegexOptions.IgnoreCase);

                // Remove script and style elements from the document
                htmlDoc.DocumentNode.InnerHtml = scriptAndStyleRegex.Replace(htmlDoc.DocumentNode.InnerHtml, "");

                // Return the cleaned HTML content
                return htmlDoc.DocumentNode.InnerHtml;
            }
            catch (HttpRequestException e)
            {
                // Handle any exceptions that occur during the HTTP request
                Console.WriteLine("An error occurred while fetching the web content: " + e.Message);
                return null;
            }
        }
    }
}
