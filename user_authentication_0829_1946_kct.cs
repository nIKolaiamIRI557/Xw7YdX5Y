// 代码生成时间: 2025-08-29 19:46:35
using System;
# FIXME: 处理边界情况
using System.Data.Entity;
# NOTE: 重要实现细节
using System.Linq;
using System.Security.Cryptography;
using System.Text;

// 用户模型
# TODO: 优化性能
public class User
# 增强安全性
{
# 添加错误处理
    public int Id { get; set; }
    public string Username { get; set; }
    public string PasswordHash { get; set; }
# TODO: 优化性能
    public string Salt { get; set; }
}

// 数据库上下文
public class AuthenticationDbContext : DbContext
# 添加错误处理
{
    public DbSet<User> Users { get; set; }

    public AuthenticationDbContext() : base("name=AuthenticationConnection")
    {
# 添加错误处理
    }

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
# FIXME: 处理边界情况
    {
        modelBuilder.Entity<User>()
            .HasKey(u => u.Id);
    }
}

// 用户身份认证服务
# TODO: 优化性能
public class AuthenticationService
{
# TODO: 优化性能
    private readonly AuthenticationDbContext _context;

    public AuthenticationService(AuthenticationDbContext context)
    {
        _context = context;
# 改进用户体验
    }

    // 注册新用户
# 优化算法效率
    public bool RegisterUser(string username, string password)
    {
        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            throw new ArgumentException("Username and password cannot be null or empty.");
        }

        var existingUser = _context.Users.FirstOrDefault(u => u.Username == username);
        if (existingUser != null)
        {
            throw new InvalidOperationException("Username already exists.");
# NOTE: 重要实现细节
        }

        var salt = GenerateSalt();
        var passwordHash = HashPassword(password, salt);

        var newUser = new User
        {
            Username = username,
            PasswordHash = passwordHash,
            Salt = salt
        };

        _context.Users.Add(newUser);
        _context.SaveChanges();

        return true;
# 扩展功能模块
    }

    // 用户登录
    public bool AuthenticateUser(string username, string password)
    {
        var user = _context.Users.FirstOrDefault(u => u.Username == username);
        if (user == null)
        {
            return false;
        }

        var enteredPasswordHash = HashPassword(password, user.Salt);
        return user.PasswordHash == enteredPasswordHash;
    }

    // 生成盐值
    private string GenerateSalt()
    {
        var buffer = new byte[256];
# 增强安全性
        using (var rng = new RNGCryptoServiceProvider())
        {
            rng.GetBytes(buffer);
        }

        return Convert.ToBase64String(buffer);
    }

    // 哈希密码
    private string HashPassword(string password, string salt)
# 改进用户体验
    {
        using (var sha256 = SHA256.Create())
# 添加错误处理
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(password);
            var saltBytes = Convert.FromBase64String(salt);
# 改进用户体验

            var plainTextWithSaltBytes = plainTextBytes.Concat(saltBytes).ToArray();

            return Convert.ToBase64String(sha256.ComputeHash(plainTextWithSaltBytes));
        }
# TODO: 优化性能
    }
}
