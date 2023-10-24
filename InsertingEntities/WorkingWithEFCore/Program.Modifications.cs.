using Microsoft.EntityFrameworkCore; // ExecuteUpdate, ExecuteDelete
using Microsoft.EntityFrameworkCore.ChangeTracking; // EntityEntry<T>
using Packt.Shared; // Northwind, Product
partial class Program
{
 static void ListProducts(int[]? productIdsToHighlight = null)
 {
 using (Northwind db = new())
 {
 if ((db.Products is null) || (!db.Products.Any()))
 {
 Fail("There are no products.");
 return;
 }
 WriteLine("| {0,-3} | {1,-35} | {2,8} | {3,5} | {4} |",
 "Id", "Product Name", "Cost", "Stock", "Disc.");
 foreach (Product p in db.Products)
 {
 ConsoleColor previousColor = ForegroundColor;
 if ((productIdsToHighlight is not null) &&
 productIdsToHighlight.Contains(p.ProductId))
 {
 ForegroundColor = ConsoleColor.Green;
 }
 WriteLine("| {0:000} | {1,-35} | {2,8:$#,##0.00} | {3,5} | {4} |",
 p.ProductId, p.ProductName, p.Cost, p.Stock, p.Discontinued);
 ForegroundColor = previousColor;
 }
 }
 }
}