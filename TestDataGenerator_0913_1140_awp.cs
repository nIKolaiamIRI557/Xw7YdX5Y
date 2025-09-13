// 代码生成时间: 2025-09-13 11:40:56
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

// 实体类
public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}

// DbContext 类
public class MyDbContext : DbContext
{
    public DbSet<Product> Products { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // 设置数据库连接字符串
        optionsBuilder.UseSqlServer("Server=(localdb)\mssqllocaldb;Database=MyDatabase;Trusted_Connection=True;");
    }
}

// 测试数据生成器类
public class TestDataGenerator
{
    private readonly MyDbContext _context;

    // 构造函数注入DbContext
    public TestDataGenerator(MyDbContext context)
    {
        _context = context;
    }

    // 生成测试数据
    public async Task GenerateTestDataAsync(int count)
    {
        try
        {
            if (count <= 0)
            {
                throw new ArgumentException("Count must be greater than 0");
            }

            // 清除现有数据
            await _context.Products.DeleteFromQueryAsync();
            await _context.SaveChangesAsync();

            // 创建测试数据
            var products = new List<Product>();
            for (int i = 1; i <= count; i++)
            {
                products.Add(new Product
                {
                    Name = $"Product {i}",
                    Price = i * 10,
                    Quantity = i * 5
                });
            }

            // 添加测试数据到数据库
            _context.Products.AddRange(products);
            await _context.SaveChangesAsync();

            Console.WriteLine($"Generated {count} test data records.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error generating test data: {ex.Message}");
        }
    }
}

// 程序入口类
class Program
{
    static async Task Main(string[] args)
    {
        // 创建DbContext实例
        using (var context = new MyDbContext())
        {
            // 确保数据库存在
            await context.Database.EnsureCreatedAsync();

            // 创建测试数据生成器实例
            var testDataGenerator = new TestDataGenerator(context);

            // 生成测试数据
            await testDataGenerator.GenerateTestDataAsync(100);
        }
    }
}