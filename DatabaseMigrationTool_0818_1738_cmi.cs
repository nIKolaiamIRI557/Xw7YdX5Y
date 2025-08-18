// 代码生成时间: 2025-08-18 17:38:33
 * Author: [Your Name]
 * Date: [Today's Date]
 */

using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using System.IO;

namespace DatabaseMigrationTool
{
    public class MigrationContext : DbContext
    {
        public MigrationContext(DbContextOptions<MigrationContext> options) : base(options)
        {
        }
    }

    public class MigrationTool
    {
        private readonly string _connectionString;

        public MigrationTool(string connectionString)
        {
            _connectionString = connectionString;
        }

        /// <summary>
        /// Applies pending database migrations.
        /// </summary>
        public void ApplyMigrations()
        {
            try
            {
                var optionsBuilder = new DbContextOptionsBuilder<MigrationContext>();
                optionsBuilder.UseSqlServer(_connectionString);
                var context = new MigrationContext(optionsBuilder.Options);
                context.Database.Migrate();
                Console.WriteLine("Migrations applied successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while applying migrations: {ex.Message}");
            }
        }

        /// <summary>
        /// Generates a new migration based on the current model differences.
        /// </summary>
        /// <param name="migrationName">The name of the migration to create.</param>
        public void GenerateMigration(string migrationName)
        {
            try
            {
                var optionsBuilder = new DbContextOptionsBuilder<MigrationContext>();
                optionsBuilder.UseSqlServer(_connectionString);
                var context = new MigrationContext(optionsBuilder.Options);
                context.Database.Migrate();
                var migrationBuilder = new MigrationBuilder();
                migrationBuilder.BuildTargetModel(context);
                migrationBuilder.GenerateScripts("Migration", migrationName, true);
                Console.WriteLine($"Migration created: {migrationName}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while generating migration: {ex.Message}");
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "Your connection string here";
            var migrationTool = new MigrationTool(connectionString);

            // Apply migrations
            migrationTool.ApplyMigrations();

            // Generate a new migration named 'InitialCreate'
            migrationTool.GenerateMigration("InitialCreate");
        }
    }
}
