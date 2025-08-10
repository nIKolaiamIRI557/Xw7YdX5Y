// 代码生成时间: 2025-08-10 19:31:40
using System;
using System.Diagnostics;
using System.Linq;
using System.Collections.Generic;

// 进程管理器类
public class ProcessManager
{
    // 获取所有进程的列表
    public List<Process> GetAllProcesses()
    {
        try
        {
            // 使用Process.GetProcesses()获取所有进程
            return Process.GetProcesses().ToList();
        }
        catch (Exception ex)
        {
            // 处理异常
            Console.WriteLine($"获取进程列表失败: {ex.Message}");
            return null;
        }
    }

    // 启动新的进程
    public Process StartProcess(string fileName, string arguments)
    {
        try
        {
            // 创建ProcessStartInfo对象
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = fileName,
                Arguments = arguments,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true
            };

            // 创建新的进程
            Process process = new Process
            {
                StartInfo = startInfo
            };

            // 启动进程
            process.Start();
            return process;
        }
        catch (Exception ex)
        {
            // 处理异常
            Console.WriteLine($"启动进程失败: {ex.Message}");
            return null;
        }
    }

    // 终止指定的进程
    public bool TerminateProcess(int processId)
    {
        try
        {
            // 使用Process.GetProcessById()获取指定进程
            Process process = Process.GetProcessById(processId);

            // 终止进程
            process.Kill();
            return true;
        }
        catch (Exception ex)
        {
            // 处理异常
            Console.WriteLine($"终止进程失败: {ex.Message}");
            return false;
        }
    }
}
