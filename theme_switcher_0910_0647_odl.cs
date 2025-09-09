// 代码生成时间: 2025-09-10 06:47:17
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

// 主题切换功能使用的数据库上下文
public class ThemeContext : DbContext
{
    public DbSet<Theme> Themes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("YourConnectionString");
    }
}

// 主题数据库模型
public class Theme
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string ColorScheme { get; set; } = null!;
}

// 主题切换服务
public class ThemeService
{
    private readonly ThemeContext _context;

    public ThemeService(ThemeContext context)
    {
        _context = context;
    }

    // 获取所有可用的主题
    public IQueryable<Theme> GetAllThemes()
    {
        return _context.Themes;
    }

    // 根据主题ID获取指定的主题
    public Theme? GetThemeById(int id)
    {
        return _context.Themes.FirstOrDefault(t => t.Id == id);
    }

    // 添加新的主题
    public Theme AddTheme(string name, string colorScheme)
    {
        var theme = new Theme { Name = name, ColorScheme = colorScheme };
        _context.Themes.Add(theme);
        _context.SaveChanges();
        return theme;
    }

    // 更新主题信息
    public bool UpdateTheme(int id, string newName, string newColorScheme)
    {
        var theme = _context.Themes.FirstOrDefault(t => t.Id == id);
        if (theme == null) return false;
        theme.Name = newName;
        theme.ColorScheme = newColorScheme;
        _context.SaveChanges();
        return true;
    }

    // 删除主题
    public bool DeleteTheme(int id)
    {
        var theme = _context.Themes.FirstOrDefault(t => t.Id == id);
        if (theme == null) return false;
        _context.Themes.Remove(theme);
        _context.SaveChanges();
        return true;
    }
}

// 主题切换功能的控制器
public class ThemeController
{
    private readonly ThemeService _service;

    public ThemeController(ThemeService service)
    {
        _service = service;
    }

    // 获取所有主题
    public IQueryable<Theme> GetAll()
    {
        return _service.GetAllThemes();
    }

    // 获取指定主题
    public Theme? GetById(int id)
    {
        return _service.GetThemeById(id);
    }

    // 添加新主题
    public Theme AddTheme(string name, string colorScheme)
    {
        try
        {
            return _service.AddTheme(name, colorScheme);
        }
        catch (Exception ex)
        {
            // 错误处理
            Console.WriteLine($"Error adding theme: {ex.Message}");
            throw;
        }
    }

    // 更新主题
    public bool UpdateTheme(int id, string newName, string newColorScheme)
    {
        try
        {
            return _service.UpdateTheme(id, newName, newColorScheme);
        }
        catch (Exception ex)
        {
            // 错误处理
            Console.WriteLine($"Error updating theme: {ex.Message}");
            throw;
        }
    }

    // 删除主题
    public bool DeleteTheme(int id)
    {
        try
        {
            return _service.DeleteTheme(id);
        }
        catch (Exception ex)
        {
            // 错误处理
            Console.WriteLine($"Error deleting theme: {ex.Message}");
            throw;
        }
    }
}
