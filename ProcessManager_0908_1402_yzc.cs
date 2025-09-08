// 代码生成时间: 2025-09-08 14:02:16
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.EntityFrameworkCore;

// 定义ProcessManager类，用于管理进程
public class ProcessManager
{
    private readonly DbContext _context;

    // 构造函数，注入数据库上下文
    public ProcessManager(DbContext context)
    {
        _context = context;
    }

    // 获取所有进程信息
    public List<ProcessInfo> GetAllProcesses()
    {
        try
        {
            // 从数据库中查询所有进程
            var processes = _context.Set<ProcessInfo>().ToList();
            return processes;
        }
        catch (Exception ex)
        {
            // 处理查询过程中的异常
            Console.WriteLine($"Error retrieving processes: {ex.Message}");
            return new List<ProcessInfo>();
        }
    }

    // 启动新进程
    public bool StartProcess(string command)
    {
        try
        {
            // 使用Process.Start启动进程
            Process.Start(command);
            return true;
        }
        catch (Exception ex)
        {
            // 处理启动进程过程中的异常
            Console.WriteLine($"Error starting process: {ex.Message}");
            return false;
        }
    }

    // 终止指定进程
    public bool TerminateProcess(int processId)
    {
        try
        {
            // 从数据库中查询指定进程
            var process = _context.Set<ProcessInfo>()
                .FirstOrDefault(p => p.Id == processId);

            if (process == null)
            {
                Console.WriteLine("Process not found.");
                return false;
            }

            // 使用Process.Kill终止进程
            Process.GetProcessById(process.Id).Kill();
            return true;
        }
        catch (Exception ex)
        {
            // 处理终止进程过程中的异常
            Console.WriteLine($"Error terminating process: {ex.Message}");
            return false;
        }
    }
}

// 定义ProcessInfo类，用于存储进程信息
public class ProcessInfo
{
    public int Id { get; set; } // 进程ID
    public string Name { get; set; } // 进程名称
    public string Command { get; set; } // 启动命令
}

// 定义ProcessContext类，用于数据库上下文
public class ProcessContext : DbContext
{
    public DbSet<ProcessInfo> Processes { get; set; }
}
