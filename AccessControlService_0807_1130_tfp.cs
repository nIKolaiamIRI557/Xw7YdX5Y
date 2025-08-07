// 代码生成时间: 2025-08-07 11:30:31
 * Maintains a simple access control mechanism which checks if a user has a specific role.
 */
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

// Assuming an Entity named User with a property Role which indicates the user's role.
public class User
{
    public int UserId { get; set; }
    public string Username { get; set; }
    public string Role { get; set; }
}

public class ApplicationContext : DbContext
{
    public DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlServer("YourConnectionStringHere");
    }
}

public class AccessControlService
{
    private readonly ApplicationContext _context;

    public AccessControlService(ApplicationContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    /*
     * Checks if a user has the specified role.
     * @param username The username of the user to check.
     * @param role The role to check for.
     * @returns True if the user has the role, otherwise false.
     */
    public bool HasRole(string username, string role)
    {
        try
        {
            var user = _context.Users
                .FirstOrDefault(u => u.Username == username);

            if (user == null)
            {
                throw new InvalidOperationException("User not found.");
            }

            return user.Role == role;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
            throw; // Rethrow the exception for the caller to handle.
        }
    }
}

/*
 * Usage example:
 * var context = new ApplicationContext();
 * var accessControlService = new AccessControlService(context);
 * bool hasAccess = accessControlService.HasRole("JohnDoe", "Admin");
 * if (hasAccess)
 * {
 *     Console.WriteLine("Access granted.");
 * }
 * else
 * {
 *     Console.WriteLine("Access denied.");
 * }
 */