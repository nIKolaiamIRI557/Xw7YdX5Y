// 代码生成时间: 2025-09-09 17:50:43
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

// 定义一个ProcessManager类，用于管理进程
public class ProcessManager
{
    // 定义一个数据库上下文
    private readonly DbContext _context;

    public ProcessManager(DbContext context)
    {
        _context = context;
    }

    // 获取所有进程信息
    public async Task<List<Process>> GetAllProcessesAsync()
    {
        try
        {
            // 使用LINQ查询数据库中的进程信息
            var processes = await _context.Set<Process>().ToListAsync();
            return processes;
        }
        catch (Exception ex)
        {
            // 捕获并记录异常
            Console.WriteLine($"Error retrieving processes: {ex.Message}");
            throw;
        }
    }

    // 启动一个新的进程
    public async Task<Process> StartProcessAsync(string processName, string arguments)
    {
        try
        {
            // 创建一个新的进程信息实体
            var process = new Process
            {
                ProcessName = processName,
                Arguments = arguments,
                StartTime = DateTime.Now,
                Status = "Running"
            };

            // 将进程信息添加到数据库
            await _context.Set<Process>().AddAsync(process);
            await _context.SaveChangesAsync();

            // 创建一个新的进程并启动
            var startInfo = new ProcessStartInfo(processName, arguments)
            {
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                RedirectStandardInput = true,
                CreateNoWindow = true
            };

            Process newProcess = new Process
            {
                StartInfo = startInfo
            };
            newProcess.Start();

            // 更新进程状态为Running
            process.Status = "Running";
            await _context.SaveChangesAsync();

            return newProcess;
        }
        catch (Exception ex)
        {
            // 捕获并记录异常
            Console.WriteLine($"Error starting process: {ex.Message}");
            throw;
        }
    }

    // 停止一个进程
    public async Task StopProcessAsync(int processId)
    {
        try
        {
            // 从数据库中获取要停止的进程信息
            var process = await _context.Set<Process>().FindAsync(processId);
            if (process == null)
            {
                throw new Exception("Process not found.");
            }

            // 停止进程
            process.Process.Kill();
            process.Status = "Stopped";
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            // 捕获并记录异常
            Console.WriteLine($"Error stopping process: {ex.Message}");
            throw;
        }
    }
}

// 定义一个进程信息实体
public class Process
{
    public int Id { get; set; }
    public string ProcessName { get; set; }
    public string Arguments { get; set; }
    public DateTime StartTime { get; set; }
    public string Status { get; set; }
    public Process Process { get; set; }
}
