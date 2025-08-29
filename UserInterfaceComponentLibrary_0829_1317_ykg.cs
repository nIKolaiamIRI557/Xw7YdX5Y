// 代码生成时间: 2025-08-29 13:17:06
 * UserInterfaceComponentLibrary.cs - A C# program using Entity Framework that represents a library of UI components.
 *
 * @author: Your Name
 * @date: current date
 * @version: 1.0
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace UserInterfaceComponentLibrary
{
    // Define the DbContext that will be used to interact with the database.
    public class UiComponentContext : DbContext
    {
        public UiComponentContext(DbContextOptions<UiComponentContext> options) : base(options)
        {
        }

        // Define the DbSet for UI Components.
        public DbSet<UiComponent> Components { get; set; }

        // Override the OnModelCreating method to configure the model.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuration for UiComponent.
            modelBuilder.Entity<UiComponent>()
                .HasKey(k => k.Id); // Primary Key
            modelBuilder.Entity<UiComponent>()
                .Property(p => p.Name)
                .IsRequired(); // Ensure the Name is not null
        }
    }

    // Define the UI Component model.
    public class UiComponent
    {
        public int Id { get; set; }
        public string Name { get; set; } // Name of the UI component
        public string Type { get; set; } // Type of the UI component, e.g., Button, TextBox
        public string Description { get; set; } // Description of the UI component
    }

    // Define the service to interact with the UI components.
    public class UiComponentService
    {
        private readonly UiComponentContext _context;

        public UiComponentService(UiComponentContext context)
        {
            _context = context;
        }

        // Add a new UI component to the library.
        public async Task AddUiComponentAsync(UiComponent component)
        {
            if (component == null) throw new ArgumentNullException(nameof(component));

            await _context.Components.AddAsync(component);
            await _context.SaveChangesAsync();
        }

        // Retrieve a list of all UI components.
        public async Task<List<UiComponent>> GetAllUiComponentsAsync()
        {
            return await _context.Components.ToListAsync();
        }

        // Retrieve a UI component by its ID.
        public async Task<UiComponent> GetUiComponentByIdAsync(int id)
        {
            return await _context.Components.FindAsync(id);
        }

        // Update a UI component.
        public async Task UpdateUiComponentAsync(UiComponent component)
        {
            if (component == null) throw new ArgumentNullException(nameof(component));

            _context.Components.Update(component);
            await _context.SaveChangesAsync();
        }

        // Delete a UI component by its ID.
        public async Task DeleteUiComponentAsync(int id)
        {
            var component = await _context.Components.FindAsync(id);
            if (component != null)
            {
                _context.Components.Remove(component);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException($"UI Component with ID {id} not found.");
            }
        }
    }
}
