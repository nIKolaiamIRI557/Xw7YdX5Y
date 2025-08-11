// 代码生成时间: 2025-08-12 01:22:14
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

// Define the Message entity
public class Message
{
    public int Id { get; set; }
    public string Content { get; set; }
    public DateTime SentDate { get; set; }
}

// Define the MessageContext as a DbContext
public class MessageContext : DbContext
{
    public DbSet<Message> Messages { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Configure the connection string to your database
        optionsBuilder.UseSqlServer("YourConnectionStringHere");
    }
}

// Define the MessageNotificationService class
public class MessageNotificationService
{
    private readonly MessageContext _context;

    public MessageNotificationService(MessageContext context)
    {
        _context = context;
    }

    // Method to add a new message
    public async Task<Message> AddMessageAsync(string content)
    {
        try
        {
            // Create a new message
            var message = new Message
            {
                Content = content,
                SentDate = DateTime.Now
            };

            // Add the message to the context and save it
            await _context.Messages.AddAsync(message);
            await _context.SaveChangesAsync();

            return message;
        }
        catch (Exception ex)
        {
            // Handle any exceptions that occur during the message creation and saving process
            Console.WriteLine($"An error occurred: {ex.Message}");
            return null;
        }
    }

    // Method to get all messages
    public async Task<List<Message>> GetAllMessagesAsync()
    {
        try
        {
            // Retrieve all messages from the database
            return await _context.Messages.ToListAsync();
        }
        catch (Exception ex)
        {
            // Handle any exceptions that occur during the message retrieval process
            Console.WriteLine($"An error occurred: {ex.Message}");
            return null;
        }
    }
}
