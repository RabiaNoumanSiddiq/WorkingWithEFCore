using Packt.Shared;

//QueryingCategories();

// FilteredIncludes();

// QueryingProducts();

//QueryingWithLike();

//GetRandomProduct();

/*
var resultAdd = AddProduct(categoryId: 6,
  productName: "Bob's Burgers", price: 500M);

if (resultAdd.affected == 1)
{
  WriteLine($"Add product successful with ID: {resultAdd.productId}.");
}

ListProducts(productIdsToHighlight: new[] { resultAdd.productId });
*/

var resultUpdate = IncreaseProductPrice(
 productNameStartsWith: "Bob", amount: 20M);
if (resultUpdate.affected == 1)
{
  WriteLine("Increase price success for ID: {resultUpdate.productId}.");
}
ListProducts(productIdsToHighlight: new[] { resultUpdate.productId });