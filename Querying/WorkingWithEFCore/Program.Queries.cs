using Microsoft.EntityFrameworkCore; // Include extension method
using Packt.Shared; // Northwind, Category, Product
partial class Program
{
  static void QueryingCategories()
  {
    using (Northwind db = new())
    {
      SectionTitle("Categories and how many products they have:");
      // a query to get all categories and their related products
      IQueryable<Category>? categories = db.Categories?
      .Include(c => c.Products);
      if ((categories is null) || (!categories.Any()))
      {
        Fail("No categories found.");
        return;
      }
      // execute query and enumerate results
      foreach (Category c in categories)
      {
        WriteLine($"{c.CategoryName} has {c.Products.Count} products.");
      }
    }
  }
}