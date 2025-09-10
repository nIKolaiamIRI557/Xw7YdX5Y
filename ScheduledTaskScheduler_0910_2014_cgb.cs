// 代码生成时间: 2025-09-10 20:14:44
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

// 定时任务调度器
public class ScheduledTaskScheduler : BackgroundService
{
    private readonly ILogger<ScheduledTaskScheduler> _logger;
    private readonly Timer _timer;
    private readonly TimeSpan _timeSpan;

    public ScheduledTaskScheduler(ILogger<ScheduledTaskScheduler> logger)
    {
        _logger = logger;

        // 设置定时任务的执行间隔时间
        _timeSpan = TimeSpan.FromSeconds(10); // 例如：每10秒执行一次

        // 创建一个Timer对象
        _timer = new Timer(DoWork, null, TimeSpan.Zero, _timeSpan);
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("Scheduled Task is running.");
            // 执行定时任务
            _timer.Change(_timeSpan, _timeSpan);
            await Task.Delay(_timeSpan, stoppingToken);
        }
    }

    // 定时任务的具体工作
    private void DoWork(object state)
    {
        try
        {
            // 在这里添加你的定时任务逻辑
            _logger.LogInformation("Executing scheduled task.");
        }
        catch (Exception ex)
        {
            // 处理定时任务执行中出现的异常
            _logger.LogError(ex, "An error occurred executing the scheduled task.");
        }
    }

    public override async Task StopAsync(CancellationToken stoppingToken)
    {
        // 停止定时任务
        _timer?.Change(Timeout.Infinite, 0);
        await base.StopAsync(stoppingToken);
    }

    public override void Dispose()
    {
        _timer?.Dispose();
        base.Dispose();
    }
}
