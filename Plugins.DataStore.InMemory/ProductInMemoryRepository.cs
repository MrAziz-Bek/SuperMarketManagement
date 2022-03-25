using System;
using CoreBusiness;
using UseCases.DataStorePluginInterfaces;

namespace Plugins.DataStore.InMemory;
public class ProductInMemoryRepository : IProductRepository
{
    private List<Product> _products;

    public ProductInMemoryRepository()
    {
        _products = new List<Product>()
        {
            new Product() { ProductId = 1, CategoryId = 1, Name = "Iced Tea", Quantity = 100, Price = 1.99 },
            new Product() { ProductId = 2, CategoryId = 1, Name = "Canada Dry", Quantity = 200, Price = 1.99 },
            new Product() { ProductId = 3, CategoryId = 2, Name = "Whole Wheat Bread", Quantity = 300, Price = 1.50 },
            new Product() { ProductId = 4, CategoryId = 2, Name = "White Bread", Quantity = 300, Price = 1.50 }
        };
    }

    public void AddProduct(Product product)
    {
        if (_products.Any(p => p.Name.Equals(product.Name, StringComparison.OrdinalIgnoreCase)))
        {
            return;
        }
        if (_products is not null && _products.Count > 1)
        {
            var maxId = _products.Max(p => p.ProductId);
            product.ProductId = maxId + 1;
        }
        else
        {
            product.ProductId = 1;
        }
        _products.Add(product);
    }

    public IEnumerable<Product> GetProducts()
    {
        return _products;
    }

    public void UpdateProduct(Product product)
    {
        var productToUpdate = GetProductById(product.ProductId);
        if (productToUpdate is not null)
        {
            productToUpdate.Name = product.Name;
            productToUpdate.CategoryId = product.CategoryId;
            productToUpdate.Price = product.Price;
            productToUpdate.Quantity = product.Quantity;
        }
    }

    public Product GetProductById(int productId)
    {
        return _products.FirstOrDefault(p => p.ProductId == productId);
    }

    public void DeleteProduct(int productId)
    {
        _products.Remove(GetProductById(productId));
    }

    public IEnumerable<Product> GetProductsByCategoryId(int categoryId)
    {
        return _products.Where(p => p.CategoryId == categoryId).ToList();
    }
}