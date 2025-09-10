// 代码生成时间: 2025-09-11 07:52:12
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
# FIXME: 处理边界情况
using Microsoft.Extensions.Hosting;
# 改进用户体验
using Microsoft.Extensions.Logging;

// 定时任务调度器
// 该类负责创建和调度定时任务
public class ScheduledTaskManager : IHostedService, IDisposable
{
    private Timer _timer;
    private readonly ILogger<ScheduledTaskManager> _logger;
    private readonly TimeSpan _interval;
# FIXME: 处理边界情况
    private readonly Action _task;

    // 构造函数
    public ScheduledTaskManager(ILogger<ScheduledTaskManager> logger, TimeSpan interval, Action task)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _interval = interval;
        _task = task ?? throw new ArgumentNullException(nameof(task));
    }

    // 实现IHostedService的StartAsync方法
# 扩展功能模块
    public Task StartAsync(CancellationToken cancellationToken)
    {
        _timer = new Timer(OnTimerElapsed, null, TimeSpan.Zero, _interval);
        return Task.CompletedTask;
    }

    // 实现IHostedService的StopAsync方法
# TODO: 优化性能
    public async Task StopAsync(CancellationToken cancellationToken)
    {
        _timer?.Change(Timeout.Infinite, 0);
        await Task.CompletedTask;
    }

    // 当定时器触发时执行的方法
    private void OnTimerElapsed(object sender, ElapsedEventArgs e)
    {
        try
# 添加错误处理
        {
            _logger.LogInformation($"Executing scheduled task at {DateTime.Now}");
            _task();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while executing the scheduled task");
# 增强安全性
        }
    }

    // 实现IDisposable接口
    public void Dispose()
    {
        _timer?.Dispose();
    }
}

// 定时任务调度器的启动器
// 该类负责将定时任务调度器注册到依赖注入容器
public static class ScheduledTaskManagerExtensions
{
    public static void AddScheduledTask<T>(this IServiceCollection services, TimeSpan interval, Action<T> task) where T : class
    {
        services.AddSingleton<IHostedService>(sp =>
        {
            var logger = sp.GetRequiredService<ILogger<T>>();
            return new ScheduledTaskManager(logger, interval, () => task(sp.GetRequiredService<T>()));
        });
    }
}