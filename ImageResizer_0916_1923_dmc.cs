// 代码生成时间: 2025-09-16 19:23:09
using System;
using System.IO;
using System.Drawing;
using System.Linq;
using System.Collections.Generic;

// 图片尺寸批量调整器
public class ImageResizer
{
    // 构造函数，接收目标目录和输出目录
    public ImageResizer(string sourceDirectory, string targetDirectory)
    {
        this.SourceDirectory = sourceDirectory;
        this.TargetDirectory = targetDirectory;
    }

    // 源目录路径
    private string SourceDirectory { get; }

    // 目标目录路径
    private string TargetDirectory { get; }

    // 调整图片尺寸的方法
    public void ResizeImages(int maxWidth, int maxHeight)
    {
        // 检查目录是否存在
        if (!Directory.Exists(SourceDirectory))
        {
            throw new DirectoryNotFoundException($"Source directory {SourceDirectory} not found.");
        }

        if (!Directory.Exists(TargetDirectory))
        {
            Directory.CreateDirectory(TargetDirectory);
        }

        // 获取所有图片文件
        var imageFiles = Directory.GetFiles(SourceDirectory, "*").Where(file => Image.FromFile(file).RawFormat.Equals(ImageFormat.Jpeg) || Image.FromFile(file).RawFormat.Equals(ImageFormat.Png));

        foreach (var file in imageFiles)
        {
            try
            {
                // 加载图片
                using (var image = Image.FromFile(file))
                {
                    // 计算新尺寸
                    var ratio = Math.Min(maxWidth / (double)image.Width, maxHeight / (double)image.Height);
                    int newWidth = (int)(image.Width * ratio);
                    int newHeight = (int)(image.Height * ratio);

                    // 生成目标文件路径
                    var targetFile = Path.Combine(TargetDirectory, Path.GetFileName(file));

                    // 创建新图片
                    using (var resizedImage = new Bitmap(newWidth, newHeight))
                    {
                        using (var g = Graphics.FromImage(resizedImage))
                        {
                            // 设置高质量输出
                            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                            g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;

                            // 绘制调整后的图片
                            g.DrawImage(image, 0, 0, newWidth, newHeight);

                            // 保存调整后的图片
                            resizedImage.Save(targetFile, image.RawFormat);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error resizing image {file}: {ex.Message}");
            }
        }
    }
}
