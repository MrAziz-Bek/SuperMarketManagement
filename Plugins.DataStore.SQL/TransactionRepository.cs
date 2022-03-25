using System;
using CoreBusiness;
using UseCases.DataStorePluginInterfaces;

namespace Plugins.DataStore.SQL;
public class TransactionRepository : ITransactionRepository
{
    private readonly MarketContext _marketContext;

    public TransactionRepository(MarketContext marketContext)
    {
        _marketContext = marketContext;
    }

    public IEnumerable<Transaction> Get(string cashierName)
    {
        return _marketContext.Transactions.Where(t => t.CashierName.ToLower() == cashierName.ToLower()).ToList();
    }

    public IEnumerable<Transaction> GetByDay(string cashierName, DateTime date)
    {
        if (string.IsNullOrWhiteSpace(cashierName))
        {
            return _marketContext.Transactions.Where(t => t.TimeStamp.Date == date.Date);
        }
        return _marketContext.Transactions.Where(t =>
                t.CashierName.ToLower() == cashierName.ToLower()
                && t.TimeStamp.Date == date.Date); ;
    }

    public void Save(string cashierName, int productId, string productName, double price, int beforeQuantity, int soldQuantity)
    {
        var transaction = new Transaction()
        {
            ProductId = productId,
            ProductName = productName,
            TimeStamp = DateTime.Now,
            Price = price,
            beforeQuantity = beforeQuantity,
            soldQuantity = soldQuantity,
            CashierName = cashierName
        };
        _marketContext.Transactions.Add(transaction);
        _marketContext.SaveChanges();
    }

    public IEnumerable<Transaction> Search(string cashierName, DateTime startDate, DateTime endDate)
    {
        if (string.IsNullOrWhiteSpace(cashierName))
        {
            return _marketContext.Transactions.Where(t => t.TimeStamp.Date >= startDate.Date && t.TimeStamp <= endDate.AddDays(1).Date);
        }
        return _marketContext.Transactions.Where(t =>
                t.CashierName.ToLower() == cashierName.ToLower()
                && t.TimeStamp.Date >= startDate.Date && t.TimeStamp <= endDate.AddDays(1).Date);
    }
}