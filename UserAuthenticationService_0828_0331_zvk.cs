// 代码生成时间: 2025-08-28 03:31:12
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

// 定义用户身份实体
public class ApplicationUser : IdentityUser {
    public string FirstName { get; set; }
    public string LastName { get; set; }
}

// 定义用户身份上下文
public class AppDbContext : IdentityDbContext<ApplicationUser> {
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {
    }

    protected override void OnModelCreating(ModelBuilder builder) {
        base.OnModelCreating(builder);
        // 自定义模型配置
    }
}

// 用户身份认证服务
public class UserAuthenticationService {
    private readonly AppDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    // 构造函数注入
    public UserAuthenticationService(AppDbContext context, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager) {
        _context = context;
        _userManager = userManager;
        _signInManager = signInManager;
    }

    // 用户登录方法
    public async Task<bool> LoginAsync(string username, string password) {
        try {
            var user = await _userManager.FindByNameAsync(username);
            if (user != null) {
                var result = await _signInManager.PasswordSignInAsync(user, password, false, false);
                return result.Succeeded;
            }
            return false;
        } catch (Exception ex) {
            // 错误处理
            Console.WriteLine($"Error during login: {ex.Message}");
            return false;
        }
    }

    // 用户注册方法
    public async Task<bool> RegisterAsync(string username, string password, string firstName, string lastName) {
        try {
            var user = new ApplicationUser { UserName = username, FirstName = firstName, LastName = lastName };
            var result = await _userManager.CreateAsync(user, password);
            if (result.Succeeded) {
                return true;
            } else {
                // 错误处理
                Console.WriteLine($"Error during registration: {string.Join(",", result.Errors.Select(e => e.Description))}");
                return false;
            }
        } catch (Exception ex) {
            // 错误处理
            Console.WriteLine($"Error during registration: {ex.Message}");
            return false;
        }
    }
}
