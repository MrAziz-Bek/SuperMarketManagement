using System;
using UseCases.DataStorePluginInterfaces;

namespace UseCases;
public class DeleteProductUseCase : IDeleteProductUseCase
{
    private readonly IProductRepository _productRepository;

    public DeleteProductUseCase(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public void Execute(int productId)
    {
        _productRepository.DeleteProduct(productId);
    }
}