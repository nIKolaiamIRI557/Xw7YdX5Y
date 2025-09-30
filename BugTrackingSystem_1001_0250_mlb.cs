// 代码生成时间: 2025-10-01 02:50:25
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

// 定义缺陷实体
public class Bug
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Status { get; set; }   // 缺陷状态，如：Open, InProgress, Resolved
    public DateTime CreatedAt { get; set; }
    public DateTime? ResolvedAt { get; set; }
}

// 定义数据库上下文
public class BugTrackingContext : DbContext
{
    public DbSet<Bug> Bugs { get; set; }

    public BugTrackingContext() : base("name=BugTrackingContext")
    {
    }
}

public class BugService
{
    private readonly BugTrackingContext _context;

    public BugService(BugTrackingContext context)
    {
        _context = context;
    }

    // 添加缺陷
    public int AddBug(Bug bug)
    {
        try
        {
            _context.Bugs.Add(bug);
            _context.SaveChanges();
            return bug.Id;
        }
        catch (Exception ex)
        {
            // 处理异常，记录日志等
            Console.WriteLine($"Error adding bug: {ex.Message}");
            throw;
        }
    }

    // 查询所有缺陷
    public List<Bug> GetAllBugs()
    {
        return _context.Bugs.ToList();
    }

    // 根据ID查询缺陷
    public Bug GetBugById(int id)
    {
        return _context.Bugs.FirstOrDefault(b => b.Id == id);
    }

    // 更新缺陷状态
    public void UpdateBugStatus(int id, string newStatus)
    {
        var bug = _context.Bugs.FirstOrDefault(b => b.Id == id);
        if (bug != null)
        {
            bug.Status = newStatus;
            _context.SaveChanges();
        }
        else
        {
            throw new InvalidOperationException($"Bug with ID {id} not found.");
        }
    }

    // 解决缺陷
    public void ResolveBug(int id)
    {
        var bug = _context.Bugs.FirstOrDefault(b => b.Id == id);
        if (bug != null)
        {
            bug.Status = "Resolved";
            bug.ResolvedAt = DateTime.Now;
            _context.SaveChanges();
        }
        else
        {
            throw new InvalidOperationException($"Bug with ID {id} not found.");
        }
    }
}
