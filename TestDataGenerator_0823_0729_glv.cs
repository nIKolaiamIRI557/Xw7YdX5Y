// 代码生成时间: 2025-08-23 07:29:01
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

// Define the context class for our database
public class MyDbContext : DbContext
{
    public DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        // Configure the database connection string here
        options.UseSqlServer("Server=(localdb)\mssqllocaldb;Database=MyDB;Trusted_Connection=True;");
    }
}

// Define the entity class for User
public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
}

// Test data generator class
public class TestDataGenerator
{
    private readonly MyDbContext _context;

    public TestDataGenerator(MyDbContext context)
    {
        _context = context;
    }

    // Method to generate test data for Users
    public void GenerateTestData(int numberOfUsers)
    {
        try
        {
            for (int i = 0; i < numberOfUsers; i++)
            {
                // Create new user instance
                var newUser = new User
                {
                    Name = $"User{i + 1}",
                    Email = $"user{i + 1}@example.com"
                };

                // Add to the context and save changes
                _context.Users.Add(newUser);
                _context.SaveChanges();
            }
        }
        catch (Exception ex)
        {
            // Handle any exceptions that may occur during data generation
            Console.WriteLine($"An error occurred while generating test data: {ex.Message}");
        }
    }
}

// Main class to run the test data generator
class Program
{
    static void Main(string[] args)
    {
        // Create a new instance of the database context
        var context = new MyDbContext();

        // Initialize the test data generator
        var testDataGenerator = new TestDataGenerator(context);

        // Generate test data for 10 users
        testDataGenerator.GenerateTestData(10);
    }
}