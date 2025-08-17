// 代码生成时间: 2025-08-17 11:48:00
using System;
using System.Diagnostics;
using System.Linq;
using Microsoft.EntityFrameworkCore;

// 定义数据库上下文
public class PerformanceDbContext : DbContext
{
    public DbSet<PerformanceData> PerformanceData { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=.;Database=PerformanceDB;Trusted_Connection=True;");
    }
}

// 定义性能数据模型
public class PerformanceData
{
    public int Id { get; set; }
    public string MetricName { get; set; }
    public long CounterValue { get; set; }
    public DateTime Timestamp { get; set; }
}

// 系统性能监控工具类
public class SystemPerformanceMonitor
{
    private readonly PerformanceDbContext _context;

    public SystemPerformanceMonitor(PerformanceDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    // 获取CPU使用率
    public double GetCpuUsage()
    {
        var cpu = new PerformanceCounter("Processor", "% Processor Time", "_Total");
        return cpu.NextValue();
    }

    // 获取内存使用情况
    public long GetMemoryUsage()
    {
        var memory = new PerformanceCounter("Memory", "Available MBytes");
        return (long)(memory.NextValue() * 1024 * 1024); // 转换为字节
    }

    // 记录性能数据到数据库
    public void RecordPerformanceData(string metricName, long counterValue)
    {
        try
        {
            var performanceData = new PerformanceData
            {
                MetricName = metricName,
                CounterValue = counterValue,
                Timestamp = DateTime.Now
            };

            _context.PerformanceData.Add(performanceData);
            _context.SaveChanges();
        }
        catch (Exception ex)
        {
            // 错误处理
            Console.WriteLine($"Error recording performance data: {ex.Message}");
        }
    }

    // 示例方法：记录CPU使用率
    public void RecordCpuUsage()
    {
        var cpuUsage = GetCpuUsage();
        RecordPerformanceData("CPU Usage", (long)cpuUsage);
    }

    // 示例方法：记录内存使用情况
    public void RecordMemoryUsage()
    {
        var memoryUsage = GetMemoryUsage();
        RecordPerformanceData("Memory Usage", memoryUsage);
    }
}
