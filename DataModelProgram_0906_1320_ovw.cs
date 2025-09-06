// 代码生成时间: 2025-09-06 13:20:05
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

// 定义一个简单的数据模型
public class Person
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime BirthDate { get; set; }
}

// 数据库上下文
public class ApplicationDbContext : DbContext
{
    public DbSet<Person> People { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        // 配置数据库连接字符串
        options.UseSqlServer("Server=(localdb)\mssqllocaldb;Database=SampleDb;Trusted_Connection=True;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // 配置实体属性
        modelBuilder.Entity<Person>().HasKey(p => p.Id);
        modelBuilder.Entity<Person>()
            .HasIndex(p => p.Name).IsUnique();

        // 可以添加更多的模型配置代码
    }
}

// 程序入口类
public class Program
{
    public static void Main(string[] args)
    {
        using (var context = new ApplicationDbContext())
        {
            try
            {
                // 数据库迁移和创建
                context.Database.Migrate();

                // 添加新记录
                var person = new Person
                {
                    Name = "John Doe",
                    BirthDate = DateTime.Now
                };
                context.Add(person);
                context.SaveChanges();

                // 查询所有记录
                var allPeople = context.People.ToList();
                foreach (var item in allPeople)
                {
                    Console.WriteLine($"Id: {item.Id}, Name: {item.Name}, BirthDate: {item.BirthDate}");
                }
            }
            catch (Exception ex)
            {
                // 错误处理
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}