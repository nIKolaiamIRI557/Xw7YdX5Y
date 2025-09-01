// 代码生成时间: 2025-09-01 23:59:39
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

// 定义数据模型
public class LayoutEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime CreatedAt { get; set; }
}

// 定义数据库上下文
public class LayoutContext : DbContext
{
    public DbSet<LayoutEntity> Layouts { get; set; }

    public LayoutContext(DbContextOptions<LayoutContext> options) : base(options)
    {
    }

    // 配置模型和数据库映射
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<LayoutEntity>()
            .Property(e => e.Name)
            .IsRequired();
    }
}

// 定义服务类，处理响应式布局相关的业务逻辑
public class ResponsiveLayoutService
{
    private readonly LayoutContext _context;

    public ResponsiveLayoutService(LayoutContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    // 获取所有布局实体
    public IQueryable<LayoutEntity> GetAllLayouts()
    {
        return _context.Layouts;
    }

    // 添加新的布局实体
    public LayoutEntity AddLayout(LayoutEntity layout)
    {
        if (layout == null)
            throw new ArgumentNullException(nameof(layout));

        _context.Layouts.Add(layout);
        _context.SaveChanges();
        return layout;
    }

    // 根据ID获取布局实体
    public LayoutEntity GetLayoutById(int id)
    {
        return _context.Layouts.FirstOrDefault(e => e.Id == id);
    }

    // 更新布局实体
    public LayoutEntity UpdateLayout(LayoutEntity layout)
    {
        if (layout == null)
            throw new ArgumentNullException(nameof(layout));

        _context.Entry(layout).State = EntityState.Modified;
        _context.SaveChanges();
        return layout;
    }

    // 删除布局实体
    public void DeleteLayout(int id)
    {
        var layout = _context.Layouts.Find(id);
        if (layout != null)
        {
            _context.Layouts.Remove(layout);
            _context.SaveChanges();
        }
        else
        {
            throw new KeyNotFoundException($"Layout with ID {id} not found.");
        }
    }
}
