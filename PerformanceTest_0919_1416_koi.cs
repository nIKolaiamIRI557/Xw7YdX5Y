// 代码生成时间: 2025-09-19 14:16:50
using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

// 定义一个性能测试程序
public class PerformanceTest
{
    private readonly DbContext _dbContext;
    private readonly string _connectionString;

    // 构造函数
    public PerformanceTest(string connectionString)
    {
        _connectionString = connectionString;
        _dbContext = new DbContextOptionsBuilder<DbContext>()
            .UseSqlServer(_connectionString)
            .Options
            .Build();
    }

    // 执行性能测试
    public async Task PerformTestAsync()
    {
        try
        {
            using (_dbContext)
            {
                // 测试查询性能
                await QueryPerformanceTestAsync();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }

    // 查询性能测试
    private async Task QueryPerformanceTestAsync()
    {
        int numberOfIterations = 100;
        for (int i = 0; i < numberOfIterations; i++)
        {
            // 开始计时
            Stopwatch stopwatch = Stopwatch.StartNew();

            // 执行查询
            await _dbContext.Set<YourEntity>().ToListAsync();

            // 停止计时并记录查询时间
            stopwatch.Stop();
            Console.WriteLine($"Iteration {i + 1}: {stopwatch.ElapsedMilliseconds} ms");
        }
    }

    // 你可以将YourEntity替换为实际的实体类名称
}
