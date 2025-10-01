// 代码生成时间: 2025-10-02 02:37:23
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ApiTestingTool
{
    /// <summary>
    /// API测试工具，用于发送HTTP请求并处理响应。
    /// </summary>
    public class ApiTestingTool
    {
        private readonly HttpClient _httpClient;

        /// <summary>
        /// 初始化API测试工具。
        /// </summary>
        public ApiTestingTool()
        {
            _httpClient = new HttpClient();
        }

        /// <summary>
        /// 发送GET请求到指定的URL并返回响应内容。
        /// </summary>
        /// <param name="url">API的URL地址。</param>
        /// <returns>响应内容的字符串表示。</returns>
        public async Task<string> GetAsync(string url)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                return responseBody;
            }
            catch (HttpRequestException e)
            {
                // 处理HTTP请求异常
                Console.WriteLine($"An error occurred: {e.Message}");
                return null;
            }
        }

        /// <summary>
        /// 发送POST请求到指定的URL并返回响应内容。
        /// </summary>
        /// <param name="url">API的URL地址。</param>
        /// <param name="content">要发送的JSON内容。</param>
        /// <returns>响应内容的字符串表示。</returns>
        public async Task<string> PostAsync(string url, string content)
        {
            try
            {
                var jsonContent = new StringContent(content, System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _httpClient.PostAsync(url, jsonContent);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                return responseBody;
            }
            catch (HttpRequestException e)
            {
                // 处理HTTP请求异常
                Console.WriteLine($"An error occurred: {e.Message}");
                return null;
            }
        }
    }
}
