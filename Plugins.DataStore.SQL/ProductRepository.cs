using System;
using CoreBusiness;
using UseCases.DataStorePluginInterfaces;

namespace Plugins.DataStore.SQL;
public class ProductRepository : IProductRepository
{
    private readonly MarketContext _marketContext;

    public ProductRepository(MarketContext marketContext)
    {
        _marketContext = marketContext;
    }

    public void AddProduct(Product product)
    {
        _marketContext.Products.Add(product);
        _marketContext.SaveChanges();
    }

    public void DeleteProduct(int productId)
    {
        var product = _marketContext.Products.FirstOrDefault(p => p.ProductId == productId);
        if (product is not null)
        {
            _marketContext.Products.Remove(product);
            _marketContext.SaveChanges();
        }
    }

    public Product GetProductById(int productId)
    {
        return _marketContext.Products.FirstOrDefault(p => p.ProductId == productId);
    }

    public IEnumerable<Product> GetProducts()
    {
        return _marketContext.Products.ToList();
    }

    public IEnumerable<Product> GetProductsByCategoryId(int categoryId)
    {
        return _marketContext.Products.Where(p => p.CategoryId == categoryId).ToList();
    }

    public void UpdateProduct(Product product)
    {
        var prod = _marketContext.Products.FirstOrDefault(p => p.ProductId == product.ProductId);

        prod.CategoryId = product.CategoryId;
        prod.Name = product.Name;
        prod.Price = product.Price;
        prod.Quantity = product.Quantity;
    }
}