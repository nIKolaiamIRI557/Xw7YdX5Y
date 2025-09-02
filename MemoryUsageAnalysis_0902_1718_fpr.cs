// 代码生成时间: 2025-09-02 17:18:48
using System;
using System.Diagnostics;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

// 定义一个继承自DbContext的类，用于与数据库进行交互
public class MemoryDbContext : DbContext
{
    public DbSet<MemoryRecord> MemoryRecords { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        // 配置数据库连接字符串
        options.UseSqlServer("Server=(localdb)\mssqllocaldb;Database=MemoryAnalysisDb;Trusted_Connection=True;");
    }
}

// 定义一个实体类，用于存储内存使用记录
public class MemoryRecord
{
    public int Id { get; set; }
    public long ProcessId { get; set; }
    public string ProcessName { get; set; } = string.Empty;
    public long WorkingSet64 { get; set; }
    public DateTime RecordTime { get; set; }
}

// 定义一个类用于分析内存使用情况
public class MemoryUsageAnalysis
{
    private readonly MemoryDbContext _context;

    public MemoryUsageAnalysis(MemoryDbContext context)
    {
        _context = context;
    }

    // 获取当前所有内存使用记录
    public IEnumerable<MemoryRecord> GetAllMemoryRecords()
    {
        try
        {
            return _context.MemoryRecords.ToList();
        }
        catch (Exception ex)
        {
            // 错误处理
            Console.WriteLine($"Error retrieving memory records: {ex.Message}");
            return null;
        }
    }

    // 获取特定进程的内存使用记录
    public IEnumerable<MemoryRecord> GetProcessMemoryRecords(int processId)
    {
        try
        {
            return _context.MemoryRecords.Where(r => r.ProcessId == processId).ToList();
        }
        catch (Exception ex)
        {
            // 错误处理
            Console.WriteLine($"Error retrieving memory records for process {processId}: {ex.Message}");
            return null;
        }
    }

    // 添加内存使用记录
    public void AddMemoryRecord(MemoryRecord record)
    {
        try
        {
            _context.MemoryRecords.Add(record);
            _context.SaveChanges();
        }
        catch (Exception ex)
        {
            // 错误处理
            Console.WriteLine($"Error adding memory record: {ex.Message}");
        }
    }

    // 记录内存使用情况
    public void RecordMemoryUsage()
    {
        try
        {
            foreach (var process in Process.GetProcesses())
            {
                var record = new MemoryRecord
                {
                    ProcessId = process.Id,
                    ProcessName = process.ProcessName,
                    WorkingSet64 = process.WorkingSet64,
                    RecordTime = DateTime.Now
                };
                AddMemoryRecord(record);
            }
        }
        catch (Exception ex)
        {
            // 错误处理
            Console.WriteLine($"Error recording memory usage: {ex.Message}");
        }
    }
}
