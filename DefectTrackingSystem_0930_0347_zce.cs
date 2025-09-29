// 代码生成时间: 2025-09-30 03:47:24
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

// 定义缺陷实体
public class Defect
{
    public int DefectId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Status { get; set; } // 比如：Open, In Progress, Closed
    public DateTime CreatedAt { get; set; }
    public DateTime? ResolvedAt { get; set; }
}

// 定义缺陷跟踪系统的数据库上下文
public class DefectTrackingContext : DbContext
{
    public DbSet<Defect> Defects { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // 设置数据库连接字符串
        optionsBuilder.UseSqlServer("YourConnectionStringHere");
    }
}

// 缺陷跟踪系统服务
public class DefectTrackingService
{
    private readonly DefectTrackingContext _context;

    public DefectTrackingService(DefectTrackingContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    // 添加新缺陷
    public async Task AddDefectAsync(Defect defect)
    {
        if (defect == null) throw new ArgumentNullException(nameof(defect));
        await _context.Defects.AddAsync(defect);
        await _context.SaveChangesAsync();
    }

    // 更新缺陷状态
    public async Task UpdateDefectStatusAsync(int defectId, string newStatus)
    {
        var defect = await _context.Defects.FindAsync(defectId);
        if (defect == null) throw new KeyNotFoundException($"Defect with ID {defectId} not found.");
        defect.Status = newStatus;
        await _context.SaveChangesAsync();
    }

    // 获取所有缺陷
    public async Task<List<Defect>> GetAllDefectsAsync()
    {
        return await _context.Defects.ToListAsync();
    }

    // 根据ID查找缺陷
    public async Task<Defect> GetDefectByIdAsync(int defectId)
    {
        return await _context.Defects.FindAsync(defectId);
    }
}

// 程序入口点
class Program
{
    static async Task Main(string[] args)
    {
        using (var context = new DefectTrackingContext())
        {
            context.Database.EnsureCreated();

            var service = new DefectTrackingService(context);

            // 示例：添加一个新缺陷
            var newDefect = new Defect
            {
                Title = "Sample Defect",
                Description = "This is a sample defect.",
                Status = "Open",
                CreatedAt = DateTime.Now
            };
            await service.AddDefectAsync(newDefect);

            // 示例：获取所有缺陷
            var allDefects = await service.GetAllDefectsAsync();
            foreach (var defect in allDefects)
            {
                Console.WriteLine($"ID: {defect.DefectId}, Title: {defect.Title}, Status: {defect.Status}");
            }
        }
    }
}