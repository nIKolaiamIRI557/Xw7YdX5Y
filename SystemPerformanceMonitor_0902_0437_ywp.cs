// 代码生成时间: 2025-09-02 04:37:08
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

/// <summary>
/// A system performance monitor tool.
/// </summary>
public class SystemPerformanceMonitor
{
    private readonly DbContext _dbContext; // Assuming a DbContext for EF

    /// <summary>
    /// Initializes a new instance of the SystemPerformanceMonitor class.
    /// </summary>
    /// <param name="dbContext">The Entity Framework DbContext.</param>
    public SystemPerformanceMonitor(DbContext dbContext)
    {
        _dbContext = dbContext;
    }

    /// <summary>
    /// Retrieves system performance metrics.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task returning system performance metrics.</returns>
    public async Task<PerformanceMetrics> GetSystemPerformanceMetrics(CancellationToken cancellationToken)
    {
        try
        {
            var metrics = new PerformanceMetrics
            {
                CpuUsage = GetCpuUsagePercentage(),
                MemoryUsage = GetMemoryUsage()
            };

            return metrics;
        }
        catch (Exception ex)
        {
            // Log the exception details here
            Console.WriteLine($"Error retrieving system performance metrics: {ex.Message}");
            throw;
        }
    }

    /// <summary>
    /// Gets the CPU usage percentage.
    /// </summary>
    /// <returns>The CPU usage percentage.</returns>
    private double GetCpuUsagePercentage()
    {
        var totalProcessorTime = TimeSpan.FromSeconds(0);
        var idleTime = TimeSpan.FromSeconds(0);

        var timer = new Stopwatch();
        timer.Start();
        for (int i = 0; i < 5; i++) // Sample over 5 intervals for a second
        {
            var cpuCycles = Process.GetCurrentProcess().TotalProcessorTime;
            var idleCycles = TimeSpan.FromSeconds(0); // Assuming idle time is not available directly

            totalProcessorTime += cpuCycles;
            idleTime += idleCycles;
        }
        timer.Stop();

        return (double)(totalProcessorTime - idleTime).TotalSeconds / timer.Elapsed.TotalSeconds * 100;
    }

    /// <summary>
    /// Gets the memory usage in MB.
    /// </summary>
    /// <returns>The memory usage.</returns>
    private long GetMemoryUsage()
    {
        return GC.GetTotalMemory(true) / 1024 / 1024; // Convert bytes to MB
    }
}

/// <summary>
/// Represents performance metrics.
/// </summary>
public class PerformanceMetrics
{
    public double CpuUsage { get; set; }
    public long MemoryUsage { get; set; }
}