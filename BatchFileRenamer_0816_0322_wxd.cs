// 代码生成时间: 2025-08-16 03:22:33
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

// 定义一个文件重命名的类
public class BatchFileRenamer
{
    // 构造函数，接受文件目录路径
    public BatchFileRenamer(string directoryPath)
    {
        if (string.IsNullOrEmpty(directoryPath))
            throw new ArgumentException("Directory path cannot be null or empty.", nameof(directoryPath));

        this.DirectoryPath = directoryPath;
    }

    // 持有目录路径的私有字段
    private string DirectoryPath { get; }

    // 执行批量重命名的方法
    public void RenameFiles(string searchPattern, Func<string, string> renameFunc)
    {
        // 获取目录下所有匹配的文件
        var files = Directory.GetFiles(DirectoryPath, searchPattern).ToList();

        // 遍历所有文件进行重命名
        foreach (var file in files)
        {
            try
            {
                // 获取新文件名并重命名
                var newFileName = renameFunc(Path.GetFileName(file));
                var newFilePath = Path.Combine(DirectoryPath, newFileName);
                File.Move(file, newFilePath);
            }
            catch (Exception ex)
            {
                // 错误处理，记录或抛出异常
                Console.WriteLine($"Error renaming file {file}: {ex.Message}");
            }
        }
    }
}

// 示例用法
public class Program
{
    public static void Main()
    {
        try
        {
            // 创建文件重命名工具实例
            var renamer = new BatchFileRenamer(@