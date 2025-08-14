// 代码生成时间: 2025-08-15 03:36:25
using System;
using System.Text.RegularExpressions;
using System.Web;

namespace XssProtection
{
    /// <summary>
    /// XSS攻击防护服务
    /// </summary>
    public static class XssProtectionService
    {
        /// <summary>
        /// 清理文本以防止XSS攻击
        /// </summary>
        /// <param name="input">需要清理的文本</param>
        /// <returns>清理后的文本</returns>
        public static string CleanInputForXss(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                // 空或null字符串无需清理
                return input;
            }

            // 使用HttpUtility.HtmlEncode进行HTML编码，避免HTML标签被当作代码执行
            input = HttpUtility.HtmlEncode(input);

            // 允许的HTML标签，可以根据需要添加或修改
            string allowedTags = "<span><b><strong><i><em><u><ul><ol><li><p><br><hr><a><img>";

            // 使用正则表达式移除或替换不允许的标签
            input = Regex.Replace(input, "<(/*)(.*?)(>)", m =>
            {
                string tag = m.Groups[2].Value;
                if (!allowedTags.Contains(tag))
                {
                    return ""; // 移除不允许的标签
                }
                return m.Value; // 保留允许的标签
            }, RegexOptions.IgnoreCase | RegexOptions.Compiled);

            // 清理后的文本
            return input;
        }
    }
}
