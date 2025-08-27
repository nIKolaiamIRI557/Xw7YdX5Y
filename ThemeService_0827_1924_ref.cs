// 代码生成时间: 2025-08-27 19:24:47
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ThemeSwitchApplication
{
    // Define a Theme entity representing different themes
    public class Theme
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    // Define a DbContext for handling database operations
    public class ThemeDbContext : DbContext
    {
        public ThemeDbContext(DbContextOptions<ThemeDbContext> options) : base(options)
        {
        }

        public DbSet<Theme> Themes { get; set; }
    }

    // Define a service class for theme switching
    public class ThemeService
    {
        private readonly ThemeDbContext _context;

        public ThemeService(ThemeDbContext context)
        {
            _context = context;
        }

        // Method to switch to a new theme by name
        public async Task<Theme> SwitchThemeAsync(string themeName)
        {
            if (string.IsNullOrWhiteSpace(themeName))
            {
                throw new ArgumentException("Theme name cannot be null or whitespace.", nameof(themeName));
            }

            var theme = await _context.Themes
                .FirstOrDefaultAsync(t => t.Name.Equals(themeName, StringComparison.OrdinalIgnoreCase));

            if (theme == null)
            {
                throw new InvalidOperationException($"Theme '{themeName}' not found.");
            }

            // Additional logic to apply the theme can be added here
            // For example, updating the user's theme preference in the database

            return theme;
        }

        // Method to get all available themes
        public async Task<IEnumerable<Theme>> GetAllThemesAsync()
        {
            return await _context.Themes.ToListAsync();
        }
    }
}
