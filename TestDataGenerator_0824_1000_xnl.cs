// 代码生成时间: 2025-08-24 10:00:27
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// 定义实体类
public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public DateTime CreatedAt { get; set; }
}

// 测试数据生成器类
public class TestDataGenerator
{
    private readonly DbContext _context;

    // 构造函数
    public TestDataGenerator(DbContext context)
    {
        _context = context;
    }

    // 生成测试数据
    public async Task GenerateTestDataAsync(int count)
    {
        try
        {
            // 检查数据库中是否已有数据
            if (await _context.Set<Product>().AnyAsync())
            {
                throw new InvalidOperationException("数据库中已有数据，无法生成测试数据。");
            }

            // 生成测试数据
            var products = new List<Product>();
            for (int i = 1; i <= count; i++)
            {
                products.Add(new Product
                {
                    Name = $"Product {i}",
                    Price = i * 10,
                    CreatedAt = DateTime.Now
                });
            }

            // 将测试数据添加到数据库
            await _context.Set<Product>().AddRangeAsync(products);
            await _context.SaveChangesAsync();

            Console.WriteLine($"成功生成 {count} 条测试数据。");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"生成测试数据时发生错误：{ex.Message}");
        }
    }
}
