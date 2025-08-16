// 代码生成时间: 2025-08-16 17:12:02
{
    "using System;"
    "using System.Linq;"
    "using Microsoft.EntityFrameworkCore;"
    "
" + "// Define the Database Context"
    "public class ApplicationDbContext : DbContext"
    "{
"    public DbSet<Theme> Themes { get; set; }
"
" + "    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)"
    "    {
"        if (!optionsBuilder.IsConfigured)"
    "        {
"            optionsBuilder.UseSqlServer("Server=(localdb)\mssqllocaldb;Database=ThemeDb;Trusted_Connection=True;");"
    "        }
"    }
"
" + "    protected override void OnModelCreating(ModelBuilder modelBuilder)"
    "    {
"        modelBuilder.Entity<Theme>()
"    "            .HasData(new Theme { Id = 1, Name = "Light" })
"    "            .HasData(new Theme { Id = 2, Name = "Dark" });
"    "    }
"
" + "}"
"
" + "// Define the Theme entity"
    "public class Theme"
    "{
"    public int Id { get; set; }
"
" + "    // Name of the theme"
    "    public string Name { get; set; }
"
" + "}"
"
" + "// ThemeService class to handle theme operations"
    "public class ThemeService"
    "{
"    private readonly ApplicationDbContext _context;
"
"    public ThemeService(ApplicationDbContext context)"
    "    {
"        _context = context;
"    }
"
" + "    // Get all themes"
    "    public IQueryable<Theme> GetAllThemes()"
    "    {
"        try"
    "        {
"            return _context.Themes;
"        }
" + "        catch (Exception ex)"
    "        {
"            // Handle exception and log error"
"            Console.WriteLine("Error fetching themes: " + ex.Message);"
"            return null;
"        }
"    }
"
" + "    // Switch theme by Id"
    "    public Theme SwitchTheme(int themeId)"
    "    {
"        try"
    "        {
"            var theme = _context.Themes.FirstOrDefault(t => t.Id == themeId);
"
" + "            if (theme == null)"
    "            {
"                throw new Exception("Theme not found.");"
"            }
"
" + "            // Simulate switching theme"
"            Console.WriteLine("Switched to theme: " + theme.Name);"
"
" + "            return theme;
"    "        }
" + "        catch (Exception ex)"
    "        {
"            Console.WriteLine("Error switching theme: " + ex.Message);"
"            return null;
"        }
"    }
"
" + "}"
}