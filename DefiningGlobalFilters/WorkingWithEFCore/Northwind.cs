using Microsoft.EntityFrameworkCore; // DbContext,DbContextOptionsBuilder

namespace Packt.Shared;
// this manages the connection to the database
public class Northwind : DbContext
{
  protected override void OnConfiguring(
  DbContextOptionsBuilder optionsBuilder)
  {
    string path = Path.Combine(
    Environment.CurrentDirectory, "Northwind.db");
    string connection = $"Filename={path}";
    ConsoleColor previousColor = ForegroundColor;
    ForegroundColor = ConsoleColor.DarkYellow;
    WriteLine($"Connection: {connection}");
    ForegroundColor = previousColor;
    optionsBuilder.UseSqlite(connection);
    optionsBuilder.LogTo(WriteLine, // Console
    new[] { RelationalEventId.CommandExecuting })
    .EnableSensitiveDataLogging();
  }

  protected override void OnModelCreating(
  ModelBuilder modelBuilder)
  {
    // example of using Fluent API instead of attributes
    // to limit the length of a category name to 15
    modelBuilder.Entity<Category>()
    .Property(category => category.CategoryName)
    .IsRequired() // NOT NULL
    .HasMaxLength(15);
    if (Database.ProviderName?.Contains("Sqlite") ?? false)
    {
      // added to "fix" the lack of decimal support in SQLite
      modelBuilder.Entity<Product>()
      .Property(product => product.Cost)
      .HasConversion<double>();
    }
    
    // global filter to remove discontinued products
    modelBuilder.Entity<Product>()
    .HasQueryFilter(p => !p.Discontinued);
  }
}