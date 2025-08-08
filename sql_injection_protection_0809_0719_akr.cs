// 代码生成时间: 2025-08-09 07:19:52
using System;
using System.Data.Entity;
using System.Linq;

// 定义数据库上下文
public class MyDbContext : DbContext
{
    public DbSet<User> Users { get; set; }

    // 构造函数
    public MyDbContext() : base("name=MyConnectionString")
    {
    }
}

// 定义用户实体
public class User
{
    public int UserId { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
}

// 用户服务类
public class UserService
{
    private readonly MyDbContext _context;

    // 构造函数注入
    public UserService(MyDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    // 获取用户列表
    public IQueryable<User> GetAllUsers()
    {
        return _context.Users;
    }

    // 根据用户名获取用户信息
    public User GetUserByUsername(string username)
    {
        if (string.IsNullOrEmpty(username))
        {
            throw new ArgumentException("Username cannot be null or empty.", nameof(username));
        }

        return _context.Users.FirstOrDefault(u => u.Username == username);
    }

    // 添加用户
    public User AddUser(User user)
    {
        if (user == null)
        {
            throw new ArgumentNullException(nameof(user));
        }

        _context.Users.Add(user);
        _context.SaveChanges();
        return user;
    }

    // 更新用户信息
    public bool UpdateUser(User user)
    {
        if (user == null)
        {
            throw new ArgumentNullException(nameof(user));
        }

        _context.Entry(user).State = EntityState.Modified;
        try
        {
            _context.SaveChanges();
            return true;
        }
        catch (DbUpdateException)
        {
            return false; // 处理并发更新失败
        }
    }

    // 删除用户
    public bool DeleteUser(int userId)
    {
        var user = _context.Users.Find(userId);
        if (user == null)
        {
            return false; // 用户不存在
        }

        _context.Users.Remove(user);
        try
        {
            _context.SaveChanges();
            return true;
        }
        catch (DbUpdateException)
        {
            return false; // 处理并发更新失败
        }
    }
}

// 程序入口点
class Program
{
    static void Main(string[] args)
    {
        using (var context = new MyDbContext())
        {
            var userService = new UserService(context);

            try
            {
                // 示例：添加用户
                var newUser = new User { Username = "newUser", Email = "newuser@example.com" };
                userService.AddUser(newUser);

                // 示例：获取用户列表
                var users = userService.GetAllUsers();
                foreach (var user in users)
                {
                    Console.WriteLine($"{user.UserId} - {user.Username} - {user.Email}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}