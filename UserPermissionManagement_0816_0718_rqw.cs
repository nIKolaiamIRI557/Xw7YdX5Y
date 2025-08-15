// 代码生成时间: 2025-08-16 07:18:10
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// 定义权限枚举，可以根据需要扩展权限种类
public enum Permission
{
    Read,
    Write,
    Edit,
    Delete
}

// 用户模型
public class User
{
    public int UserId { get; set; }
    public string Username { get; set; }
    public virtual ICollection<Role> Roles { get; set; } = new List<Role>();
}

// 角色模型
public class Role
{
    public int RoleId { get; set; }
    public string RoleName { get; set; }
    public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    public virtual ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
}

// 权限模型
public class Permission
{
    public int PermissionId { get; set; }
    public string Name { get; set; }
    public virtual ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
}

// 用户角色关系模型
public class UserRole
{
    public int UserId { get; set; }
    public User User { get; set; }
    public int RoleId { get; set; }
    public Role Role { get; set; }
}

// 角色权限关系模型
public class RolePermission
{
    public int RoleId { get; set; }
    public Role Role { get; set; }
    public int PermissionId { get; set; }
    public Permission Permission { get; set; }
}

// 数据库上下文
public class UserPermissionContext : DbContext
{
    public UserPermissionContext(DbContextOptions<UserPermissionContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Permission> Permissions { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
    public DbSet<RolePermission> RolePermissions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<UserRole>().HasKey(ur => new { ur.UserId, ur.RoleId });
        modelBuilder.Entity<UserRole>().HasOne(ur => ur.User).WithMany(u => u.Roles).HasForeignKey(ur => ur.UserId);
        modelBuilder.Entity<UserRole>().HasOne(ur => ur.Role).WithMany(r => r.UserRoles).HasForeignKey(ur => ur.RoleId);
        modelBuilder.Entity<RolePermission>().HasKey(rp => new { rp.RoleId, rp.PermissionId });
        modelBuilder.Entity<RolePermission>().HasOne(rp => rp.Role).WithMany(r => r.RolePermissions).HasForeignKey(rp => rp.RoleId);
        modelBuilder.Entity<RolePermission>().HasOne(rp => rp.Permission).WithMany(p => p.RolePermissions).HasForeignKey(rp => rp.PermissionId);
    }
}

// 用户权限管理服务
public class UserPermissionService
{
    private readonly UserPermissionContext _context;

    public UserPermissionService(UserPermissionContext context)
    {
        _context = context;
    }

    // 添加用户
    public async Task AddUserAsync(User user)
    {
        if (user == null) throw new ArgumentNullException(nameof(user));
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
    }

    // 添加角色
    public async Task AddRoleAsync(Role role)
    {
        if (role == null) throw new ArgumentNullException(nameof(role));
        await _context.Roles.AddAsync(role);
        await _context.SaveChangesAsync();
    }

    // 为角色添加权限
    public async Task AddRolePermissionAsync(int roleId, int permissionId)
    {
        var rolePermission = new RolePermission { RoleId = roleId, PermissionId = permissionId };
        await _context.RolePermissions.AddAsync(rolePermission);
        await _context.SaveChangesAsync();
    }

    // 分配用户角色
    public async Task AssignUserRoleAsync(int userId, int roleId)
    {
        var userRole = new UserRole { UserId = userId, RoleId = roleId };
        await _context.UserRoles.AddAsync(userRole);
        await _context.SaveChangesAsync();
    }

    // 检查用户是否有权限
    public async Task<bool> HasPermissionAsync(int userId, Permission permission)
    {
        var user = await _context.Users
            .Include(u => u.Roles)
            .ThenInclude(r => r.RolePermissions)
            .Where(u => u.UserId == userId)
            .FirstOrDefaultAsync();

        if (user == null) return false;

        return user.Roles.Any(r => r.RolePermissions.Any(rp => rp.Permission.Name == permission.ToString()));
    }
}
