// 代码生成时间: 2025-08-20 03:54:05
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

// 定义库存项
public class InventoryItem
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}

// 定义库存管理的DbContext
public class InventoryContext : DbContext
{
    public DbSet<InventoryItem> InventoryItems { get; set; }

    public InventoryContext() : base("name=InventoryConnectionString")
    {
    }
}

// 库存管理系统
public class InventoryManagementSystem
{
    private readonly InventoryContext _context;

    public InventoryManagementSystem(InventoryContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    // 添加库存项
    public void AddInventoryItem(InventoryItem item)
    {
        if (item == null) throw new ArgumentNullException(nameof(item));
        _context.InventoryItems.Add(item);
        _context.SaveChanges();
    }

    // 更新库存项
    public void UpdateInventoryItem(int id, InventoryItem updatedItem)
    {
        var item = _context.InventoryItems.Find(id);
        if (item == null) throw new KeyNotFoundException($"Inventory item with id {id} not found.");

        item.Name = updatedItem.Name;
        item.Quantity = updatedItem.Quantity;
        item.Price = updatedItem.Price;
        _context.SaveChanges();
    }

    // 删除库存项
    public void DeleteInventoryItem(int id)
    {
        var item = _context.InventoryItems.Find(id);
        if (item == null) throw new KeyNotFoundException($"Inventory item with id {id} not found.");

        _context.InventoryItems.Remove(item);
        _context.SaveChanges();
    }

    // 获取所有库存项
    public IEnumerable<InventoryItem> GetAllInventoryItems()
    {
        return _context.InventoryItems.ToList();
    }

    // 根据ID获取库存项
    public InventoryItem GetInventoryItemById(int id)
    {
        return _context.InventoryItems.Find(id);
    }
}
