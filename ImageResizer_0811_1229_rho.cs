// 代码生成时间: 2025-08-11 12:29:43
using System;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Collections.Generic;
using System.Linq;

namespace ImageProcessing
{
    /// <summary>
    /// 图片尺寸批量调整器
    /// </summary>
    public class ImageResizer
    {
        private readonly string _inputDirectory;
        private readonly string _outputDirectory;
        private readonly int _newWidth;
        private readonly int _newHeight;

        public ImageResizer(string inputDirectory, string outputDirectory, int newWidth, int newHeight)
        {
            _inputDirectory = inputDirectory;
            _outputDirectory = outputDirectory;
            _newWidth = newWidth;
            _newHeight = newHeight;
        }

        /// <summary>
        /// 批量调整图片尺寸
        /// </summary>
        public void ResizeImages()
        {
            try
            {
                // 获取输入目录下的所有图片
                var files = Directory.GetFiles(_inputDirectory).Where(file => IsImage(file));

                // 检查输出目录是否存在，不存在则创建
                Directory.CreateDirectory(_outputDirectory);

                // 遍历所有图片并调整尺寸
                foreach (var file in files)
                {
                    ResizeImage(file, Path.Combine(_outputDirectory, Path.GetFileName(file)));
                }
            }
            catch (Exception ex)
            {
                // 错误处理
                Console.WriteLine($"Error resizing images: {ex.Message}");
            }
        }

        /// <summary>
        /// 检查文件是否为图片
        /// </summary>
        private bool IsImage(string file)
        {
            var imageFormats = new List<ImageFormat> { ImageFormat.Jpeg, ImageFormat.Png, ImageFormat.Gif, ImageFormat.Bmp };
            return imageFormats.Any(format => file.EndsWith($".{format.FileExtensions[0]}", StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// 调整单张图片的尺寸
        /// </summary>
        private void ResizeImage(string inputFile, string outputFile)
        {
            using (var image = Image.FromFile(inputFile))
            {
                using (var resizedImage = new Bitmap(_newWidth, _newHeight))
                {
                    using (var g = Graphics.FromImage(resizedImage))
                    {
                        // 设置高质量插值法
                        g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                        // 设置高质量,低速度呈现平滑程度
                        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                        // 清除指定区域
                        g.Clear(Color.WhiteSmoke);
                        // 绘制调整后的图片
                        g.DrawImage(image, 0, 0, _newWidth, _newHeight);
                    }

                    // 保存调整后的图片
                    resizedImage.Save(outputFile, image.RawFormat);
                }
            }
        }
    }
}
