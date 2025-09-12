// 代码生成时间: 2025-09-12 21:56:56
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

// 定义库存项目实体
public class InventoryItem
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}

// 库存管理系统上下文
public class InventoryManagementContext : DbContext
{
    public DbSet<InventoryItem> InventoryItems { get; set; }

    public InventoryManagementContext() : base("name=InventoryConnectionString")
    {
        // 移除复数约定，因为库存项不需要复数名称
        Configuration.LazyLoadingEnabled = false;
        Configuration.ProxyCreationEnabled = false;
        this.Configuration.Conventions.Remove<PluralizingTableNameConvention>();
    }

    // 数据库迁移时的种子数据
    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // 配置库存项
        modelBuilder.Entity<InventoryItem>().ToTable("InventoryItems");
    }
}

// 库存管理系统服务
public class InventoryManagementService
{
    private readonly InventoryManagementContext _context;

    public InventoryManagementService(InventoryManagementContext context)
    {
        _context = context;
    }

    // 添加库存项
    public InventoryItem AddItem(InventoryItem item)
    {
        if (item == null) throw new ArgumentNullException(nameof(item));
        try
        {
            _context.InventoryItems.Add(item);
            _context.SaveChanges();
            return item;
        }
        catch (Exception ex)
        {
            // 错误处理
            Console.WriteLine("An error occurred: " + ex.Message);
            return null;
        }
    }

    // 更新库存项
    public bool UpdateItem(InventoryItem item)
    {
        if (item == null) throw new ArgumentNullException(nameof(item));
        try
        {
            _context.Entry(item).State = EntityState.Modified;
            _context.SaveChanges();
            return true;
        }
        catch (Exception ex)
        {
            // 错误处理
            Console.WriteLine("An error occurred: " + ex.Message);
            return false;
        }
    }

    // 删除库存项
    public bool RemoveItem(int id)
    {
        try
        {
            var item = _context.InventoryItems.Find(id);
            if (item != null)
            {
                _context.InventoryItems.Remove(item);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
        catch (Exception ex)
        {
            // 错误处理
            Console.WriteLine("An error occurred: " + ex.Message);
            return false;
        }
    }

    // 获取所有库存项
    public List<InventoryItem> GetAllItems()
    {
        try
        {
            return _context.InventoryItems.ToList();
        }
        catch (Exception ex)
        {
            // 错误处理
            Console.WriteLine("An error occurred: " + ex.Message);
            return null;
        }
    }

    // 通过ID查找库存项
    public InventoryItem FindItemById(int id)
    {
        try
        {
            return _context.InventoryItems.Find(id);
        }
        catch (Exception ex)
        {
            // 错误处理
            Console.WriteLine("An error occurred: " + ex.Message);
            return null;
        }
    }
}
