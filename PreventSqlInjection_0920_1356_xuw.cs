// 代码生成时间: 2025-09-20 13:56:55
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

// 定义一个简单的实体类
public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
}

// 定义DB上下文
public class ApplicationDbContext : DbContext
{
    public DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("你的数据库连接字符串");
    }
}

public class UserService
{
    private readonly ApplicationDbContext _context;

    public UserService(ApplicationDbContext context)
    {
        _context = context;
    }

    // 获取用户列表
    public async Task<List<User>> GetUsersAsync()
    {
        try
        {
            return await _context.Users.ToListAsync();
        }
        catch (Exception ex)
        {
            // 错误处理
            Console.WriteLine("Error fetching users: " + ex.Message);
            return null;
        }
    }

    // 添加用户
    public async Task<bool> AddUserAsync(User user)
    {
        try
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            // 错误处理
            Console.WriteLine("Error adding user: " + ex.Message);
            return false;
        }
    }

    // 更新用户信息
    public async Task<bool> UpdateUserAsync(User user)
    {
        try
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            // 错误处理
            Console.WriteLine("Error updating user: " + ex.Message);
            return false;
        }
    }

    // 删除用户
    public async Task<bool> DeleteUserAsync(int id)
    {
        try
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
                return true;
            }
            else
            {
                Console.WriteLine("User not found.");
                return false;
            }
        }
        catch (Exception ex)
        {
            // 错误处理
            Console.WriteLine("Error deleting user: " + ex.Message);
            return false;
        }
    }
}

// 演示如何使用UserService
public class Program
{
    public static async Task Main(string[] args)
    {
        using (var context = new ApplicationDbContext())
        {
            var userService = new UserService(context);
            try
            {
                // 获取用户列表
                var users = await userService.GetUsersAsync();
                users?.ForEach(u => Console.WriteLine($"Id: {u.Id}, Name: {u.Name}, Email: {u.Email}"));

                // 添加用户
                User newUser = new User { Name = "John Doe", Email = "john.doe@example.com" };
                bool added = await userService.AddUserAsync(newUser);
                Console.WriteLine($"User added: {added}");

                // 更新用户信息
                newUser.Name = "Jane Doe";
                bool updated = await userService.UpdateUserAsync(newUser);
                Console.WriteLine($"User updated: {updated}");

                // 删除用户
                bool deleted = await userService.DeleteUserAsync(newUser.Id);
                Console.WriteLine($"User deleted: {deleted}");
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }
    }
}