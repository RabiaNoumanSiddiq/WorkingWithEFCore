using Microsoft.EntityFrameworkCore; // Include extension method
using Microsoft.EntityFrameworkCore.ChangeTracking; // CollectionEntry
using Packt.Shared; // Northwind, Category, Product

partial class Program
{
  static void QueryingCategories()
  {
    using (Northwind db = new())
    {
      SectionTitle("Categories and how many products they have:");

      // a query to get all categories and their related products
      IQueryable<Category>? categories;
      // = db.Categories;
      //.Include(c => c.Products);

      db.ChangeTracker.LazyLoadingEnabled = false;

      Write("Enable eager loading? (Y/N): ");
      bool eagerLoading = (ReadKey(intercept: true).Key == ConsoleKey.Y);
      bool explicitLoading = false;
      WriteLine();

      if (eagerLoading)
      {
        categories = db.Categories?.Include(c => c.Products);
      }
      else
      {
        categories = db.Categories;
        Write("Enable explicit loading? (Y/N): ");
        explicitLoading = (ReadKey(intercept: true).Key == ConsoleKey.Y);
        WriteLine();
      }

      if ((categories is null) || (!categories.Any()))
      {
        Fail("No categories found.");
        return;
      }

      // execute query and enumerate results
      foreach (Category c in categories)
      {
        if (explicitLoading)
        {
          Write($"Explicitly load products for {c.CategoryName}? (Y/N): ");
          ConsoleKeyInfo key = ReadKey(intercept: true);
          WriteLine();

          if (key.Key == ConsoleKey.Y)
          {
            CollectionEntry<Category, Product> products =
              db.Entry(c).Collection(c2 => c2.Products);

            if (!products.IsLoaded) products.Load();
          }
        }

        WriteLine($"{c.CategoryName} has {c.Products.Count} products.");
      }
    }
  }

  static void FilteredIncludes()
  {
    using (Northwind db = new())
    {
      SectionTitle("Products with a minimum number of units in stock.");

      string? input;
      int stock;

      do
      {
        Write("Enter a minimum for units in stock: ");
        input = ReadLine();
      } while (!int.TryParse(input, out stock));

      IQueryable<Category>? categories = db.Categories?
        .Include(c => c.Products.Where(p => p.Stock >= stock));

      if ((categories is null) || (!categories.Any()))
      {
        Fail("No categories found.");
        return;
      }

      foreach (Category c in categories)
      {
        WriteLine($"{c.CategoryName} has {c.Products.Count} products with a minimum of {stock} units in stock.");
        foreach (Product p in c.Products)
        {
          WriteLine($"  {p.ProductName} has {p.Stock} units in stock.");
        }
      }
    }
  }
}