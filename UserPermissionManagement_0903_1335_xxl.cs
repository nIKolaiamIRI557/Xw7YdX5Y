// 代码生成时间: 2025-09-03 13:35:56
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

// 定义数据上下文
public class PermissionContext : DbContext
{
    public PermissionContext(DbContextOptions<PermissionContext> options) : base(options)
    {
    }

    // 用户表
    public DbSet<User> Users { get; set; }
    // 角色表
    public DbSet<Role> Roles { get; set; }
    // 用户角色关系表
    public DbSet<UserRole> UserRoles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<User>().ToTable("Users");
        modelBuilder.Entity<Role>().ToTable("Roles");
        modelBuilder.Entity<UserRole>().ToTable("UserRoles");
    }
}

// 用户实体
public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<UserRole> UserRoles { get; set; } = new List<UserRole>();
}

// 角色实体
public class Role
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<UserRole> UserRoles { get; set; } = new List<UserRole>();
}

// 用户角色关系实体
public class UserRole
{
    public int UserId { get; set; }
    public User User { get; set; }
    public intRoleId { get; set; }
    public Role Role { get; set; }
}

// 用户权限管理服务
public class PermissionManagementService
{
    private readonly PermissionContext _context;

    public PermissionManagementService(PermissionContext context)
    {
        _context = context;
    }

    // 添加用户
    public async Task AddUserAsync(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
    }

    // 删除用户
    public async Task DeleteUserAsync(int userId)
    {
        var user = await _context.Users.FindAsync(userId);
        if (user != null)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }
        else
        {
            throw new ArgumentException("User not found");
        }
    }

    // 为用户分配角色
    public async Task AssignRoleAsync(int userId, int roleId)
    {
        var userRole = new UserRole { UserId = userId, RoleId = roleId };
        await _context.UserRoles.AddAsync(userRole);
        await _context.SaveChangesAsync();
    }

    // 撤销用户角色
    public async Task RevokeRoleAsync(int userId, int roleId)
    {
        var userRole = await _context.UserRoles.FirstOrDefaultAsync(ur => ur.UserId == userId && ur.RoleId == roleId);
        if (userRole != null)
        {
            _context.UserRoles.Remove(userRole);
            await _context.SaveChangesAsync();
        }
        else
        {
            throw new ArgumentException("User role not found");
        }
    }
}
