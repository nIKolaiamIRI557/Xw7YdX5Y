// 代码生成时间: 2025-08-13 12:56:18
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

// 数据模型
public class SecurityAuditLog
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [MaxLength(255)]
    public string Action { get; set; }

    [Required]
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;

    [MaxLength(255)]
    public string UserId { get; set; }

    [MaxLength(1024)]
    public string Details { get; set; }
}

// DbContext
public class AuditDbContext : DbContext
{
    public DbSet<SecurityAuditLog> SecurityAuditLogs { get; set; }

    public AuditDbContext(DbContextOptions<AuditDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<SecurityAuditLog>().ToTable("SecurityAuditLogs");
    }
}

// 安全审计日志服务
public class SecurityAuditLogService
{
    private readonly AuditDbContext _context;

    public SecurityAuditLogService(AuditDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    // 添加安全审计日志记录
    public void AddLog(string action, string userId, string details)
    {
        try
        {
            var log = new SecurityAuditLog
            {
                Action = action,
                UserId = userId,
                Details = details
            };
            _context.SecurityAuditLogs.Add(log);
            _context.SaveChanges();
        }
        catch (Exception ex)
        {
            // 错误处理逻辑
            Console.WriteLine($"Error adding audit log: {ex.Message}");
        }
    }

    // 获取所有安全审计日志记录
    public IQueryable<SecurityAuditLog> GetAllLogs()
    {
        return _context.SecurityAuditLogs;
    }
}
