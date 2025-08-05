// 代码生成时间: 2025-08-06 06:30:58
 * 作者：[你的名字]
 * 日期：[当前日期]
 */

using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Linq;

namespace TextFileAnalysis
{
    public class TextFileAnalyzer
    {
        private readonly string filePath;

        // 构造函数，初始化文件路径
        public TextFileAnalyzer(string filePath)
        {
            this.filePath = filePath ?? throw new ArgumentNullException(nameof(filePath));
        }

        // 分析文件内容
        public void Analyze()
        {
            try
            {
                // 读取文件内容
                var content = File.ReadAllText(filePath);

                // 计算行数
                int lineCount = content.Split(new[] {\r
}, StringSplitOptions.None).Length;

                // 使用正则表达式匹配单词
                var wordMatches = Regex.Matches(content, @"\b\w+\b");
                int wordCount = wordMatches.Count;

                // 计算字符数量
                int charCount = content.Length;

                // 输出统计结果
                Console.WriteLine($"Line Count: {lineCount}");
                Console.WriteLine($"Word Count: {wordCount}");
                Console.WriteLine($"Character Count: {charCount}");
            }
            catch (Exception ex)
            {
                // 错误处理
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        // 程序入口点
        public static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                Console.WriteLine("Please provide a file path as an argument.");
                return;
            }

            string filePath = args[0];
            TextFileAnalyzer analyzer = new TextFileAnalyzer(filePath);
            analyzer.Analyze();
        }
    }
}