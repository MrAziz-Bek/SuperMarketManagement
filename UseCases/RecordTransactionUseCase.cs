 using System;
using UseCases.DataStorePluginInterfaces;

namespace UseCases;
public class RecordTransactionUseCase : IRecordTransactionUseCase
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly IGetProductByIdUseCase _getProductByIdUseCase;

    public RecordTransactionUseCase(ITransactionRepository transactionRepository, IGetProductByIdUseCase getProductByIdUseCase)
    {
        _transactionRepository = transactionRepository;
        _getProductByIdUseCase = getProductByIdUseCase;
    }
    public void Execute(string cashierName, int productId, int quantity)
    {
        var product = _getProductByIdUseCase.Execute(productId);
        _transactionRepository.Save(cashierName, productId, product.Name, product.Price.Value, product.Quantity.Value, quantity);
    }
}