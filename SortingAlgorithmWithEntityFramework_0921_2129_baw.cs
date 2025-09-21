// 代码生成时间: 2025-09-21 21:29:27
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

// 定义排序算法的类
public class SortingAlgorithm
{
    // 使用泛型定义排序方法
    public List<T> Sort<T>(List<T> list, Comparison<T> comparison) where T : IComparable
    {
        // 检查列表是否为空
        if (list == null)
        {
            throw new ArgumentNullException(nameof(list));
        }

        // 检查比较函数是否为空
        if (comparison == null)
        {
            throw new ArgumentNullException(nameof(comparison));
        }

        // 使用冒泡排序算法对列表进行排序
        int n = list.Count;
        for (int i = 0; i < n - 1; i++)
        {
            for (int j = 0; j < n - i - 1; j++)
            {
                if (comparison(list[j], list[j + 1]) > 0)
                {
                    // 交换元素
                    T temp = list[j];
                    list[j] = list[j + 1];
                    list[j + 1] = temp;
                }
            }
        }

        // 返回排序后的列表
        return list;
    }

    // 使用List<T>的比较器实现排序
    public List<T> Sort<T>(List<T> list) where T : IComparable
    {
        // 直接调用上面的方法，传入默认的比较器
        return Sort(list, (a, b) => a.CompareTo(b));
    }
}

// 示例实体类，用于EntityFramework
public class SortableEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Value { get; set; }
}

// 示例数据库上下文
public class SortingDbContext : DbContext
{
    public DbSet<SortableEntity> SortableEntities { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // 配置数据库连接字符串
        optionsBuilder.UseSqlServer("YourConnectionStringHere");
    }
}

// 示例用法
class Program
{
    static void Main(string[] args)
    {
        // 创建数据库上下文实例
        SortingDbContext context = new SortingDbContext();

        // 获取实体列表
        var entities = context.SortableEntities.ToList();

        // 创建排序算法实例
        var sorter = new SortingAlgorithm();

        // 根据Value属性对实体进行排序
        var sortedEntities = sorter.Sort(entities, (a, b) => a.Value.CompareTo(b.Value));

        // 输出排序结果
        foreach (var entity in sortedEntities)
        {
            Console.WriteLine($"{entity.Id} - {entity.Name} - {entity.Value}");
        }
    }
}