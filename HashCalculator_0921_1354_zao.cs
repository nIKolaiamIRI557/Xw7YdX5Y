// 代码生成时间: 2025-09-21 13:54:53
using System;
using System.Security.Cryptography;
using System.Text;

namespace HashCalculatorApp
{
    // 哈希值计算工具类
    public class HashCalculator
    {
        // 计算字符串的哈希值
        public string CalculateHash(string input, HashAlgorithm algorithm)
        {
            try
            {
                // 使用指定的哈希算法计算哈希值
                using (HashAlgorithm hashAlg = algorithm)
                {
                    // 将输入字符串编码为字节数组
                    byte[] byteData = Encoding.UTF8.GetBytes(input);
                    // 计算哈希值
                    byte[] byteHash = hashAlg.ComputeHash(byteData);
                    // 将字节数组转换为十六进制字符串
                    StringBuilder sb = new StringBuilder();
                    foreach (byte b in byteHash)
                    {
                        sb.Append(b.ToString("x2"));
                    }
                    return sb.ToString();
                }
            }
# 添加错误处理
            catch (Exception ex)
            {
                // 异常处理
                Console.WriteLine($"Error while calculating hash: {ex.Message}");
                return null;
            }
        }

        // 主程序入口点
        public static void Main(string[] args)
        {
            // 创建哈希计算工具实例
            HashCalculator calculator = new HashCalculator();

            // 输入字符串
            string input = "Hello, World!";

            // 使用SHA256算法计算哈希值
# 添加错误处理
            string sha256Hash = calculator.CalculateHash(input, SHA256.Create());
            Console.WriteLine($"SHA256 Hash: {sha256Hash}");

            // 使用MD5算法计算哈希值
            string md5Hash = calculator.CalculateHash(input, MD5.Create());
            Console.WriteLine($"MD5 Hash: {md5Hash}");
        }
# 优化算法效率
    }
}
# 优化算法效率