// 代码生成时间: 2025-08-27 13:30:16
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// 定义消息实体
public class Message
{
    public int Id { get; set; }
    public string Content { get; set; } = string.Empty;
    public DateTime SentDate { get; set; } = DateTime.UtcNow;
}

// 定义消息通知上下文
public class NotificationContext : DbContext
{
    public DbSet<Message> Messages { get; set; }

    public NotificationContext(DbContextOptions<NotificationContext> options) : base(options)
    {
    }
}

// 消息通知服务
public class MessageNotificationService
{
    private readonly NotificationContext _context;

    public MessageNotificationService(NotificationContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    // 发送消息
    public async Task<Message> SendMessageAsync(string content)
    {
        if (string.IsNullOrWhiteSpace(content))
            throw new ArgumentException("Message content cannot be null or whitespace.", nameof(content));

        var message = new Message { Content = content };

        await _context.Messages.AddAsync(message);
        await _context.SaveChangesAsync();

        return message;
    }

    // 获取所有消息
    public async Task<List<Message>> GetAllMessagesAsync()
    {
        return await _context.Messages.ToListAsync();
    }

    // 错误处理和日志记录可以在这里实现，但为了简洁，这里省略
}

// 程序入口点
public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = new DbContextOptionsBuilder<NotificationContext>();
        builder.UseSqlServer("YOUR_CONNECTION_STRING"); // 替换为实际的数据库连接字符串

        using (var context = new NotificationContext(builder.Options))
        {
            // 确保数据库存在
            context.Database.EnsureCreated();

            // 创建消息通知服务实例
            var notificationService = new MessageNotificationService(context);

            try
            {
                // 发送消息
                var message = await notificationService.SendMessageAsync("Hello, this is a test message!");
                Console.WriteLine($"Message sent with ID: {message.Id}");

                // 获取所有消息
                var messages = await notificationService.GetAllMessagesAsync();
                foreach (var msg in messages)
                {
                    Console.WriteLine($"ID: {msg.Id}, Content: {msg.Content}, Sent Date: {msg.SentDate}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}