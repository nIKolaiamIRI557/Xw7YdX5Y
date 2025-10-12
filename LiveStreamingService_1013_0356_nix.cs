// 代码生成时间: 2025-10-13 03:56:24
using System;
using System.Linq;
using System.Data.Entity;
using System.Threading.Tasks;
using Newtonsoft.Json;

// 定义直播推流实体
public class LiveStream
{
    public int Id { get; set; }
    public string StreamUrl { get; set; } // 直播流地址
    public DateTime StartTime { get; set; } // 开始时间
    public DateTime? EndTime { get; set; } // 结束时间
    public bool IsActive { get; set; } // 是否活跃
}

// 定义直播推流数据库上下文
public class LiveStreamContext : DbContext
{
    public DbSet<LiveStream> LiveStreams { get; set; }

    public LiveStreamContext() : base("name=LiveStreamContext")
    {
    }
}

// 直播推流服务
public class LiveStreamingService
{
    private readonly LiveStreamContext _context;

    public LiveStreamingService(LiveStreamContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    // 获取所有活跃的直播流
    public async Task<IQueryable<LiveStream>> GetActiveStreamsAsync()
    {
        return await _context.LiveStreams
            .Where(ls => ls.IsActive)
            .ToListAsync();
    }

    // 开始一个新的直播流
    public async Task<LiveStream> StartStreamAsync(string streamUrl)
    {
        var newStream = new LiveStream
        {
            StreamUrl = streamUrl,
            StartTime = DateTime.Now,
            IsActive = true
        };

        await _context.LiveStreams.AddAsync(newStream);
        await _context.SaveChangesAsync();

        return newStream;
    }

    // 结束一个直播流
    public async Task<LiveStream> EndStreamAsync(int streamId)
    {
        var stream = await _context.LiveStreams.FindAsync(streamId);
        if (stream == null)
        {
            throw new InvalidOperationException($"No active stream found with ID: {streamId}");
        }

        stream.EndTime = DateTime.Now;
        stream.IsActive = false;

        await _context.SaveChangesAsync();

        return stream;
    }

    // 更新直播流信息
    public async Task<LiveStream> UpdateStreamAsync(int streamId, string newStreamUrl)
    {
        var stream = await _context.LiveStreams.FindAsync(streamId);
        if (stream == null)
        {
            throw new InvalidOperationException($"No stream found with ID: {streamId}");
        }

        stream.StreamUrl = newStreamUrl;
        stream.StartTime = DateTime.Now; // Reset start time for updates

        await _context.SaveChangesAsync();

        return stream;
    }

    // 检查直播流是否存在
    public async Task<bool> StreamExistsAsync(int streamId)
    {
        return await _context.LiveStreams.AnyAsync(ls => ls.Id == streamId);
    }
}
