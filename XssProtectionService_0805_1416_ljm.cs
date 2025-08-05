// 代码生成时间: 2025-08-05 14:16:57
using System;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Html;

// 这个类提供了XSS攻击防护功能
public class XssProtectionService
{
    // 检查并清理HTML内容，防止XSS攻击
    public string CleanHtml(string html)
    {
        if (string.IsNullOrWhiteSpace(html))
        {
            // 如果输入为空或空白，直接返回
            return html;
        }

        // 使用HtmlSanitizer类来清理HTML
        var sanitizedHtml = HtmlSanitizer.Clean(html);

        // 进一步清理，移除<script>标签
        sanitizedHtml = Regex.Replace(sanitizedHtml, @