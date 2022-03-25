using CoreBusiness;
using UseCases.DataStorePluginInterfaces;

namespace Plugins.DataStore.InMemory;
public class TransactionInMemoryRepository : ITransactionRepository
{
    private readonly List<Transaction> Transactions;

    public TransactionInMemoryRepository()
    {
        Transactions = new List<Transaction>();
    }

    public IEnumerable<Transaction> Get(string cashierName)
    {
        if (string.IsNullOrWhiteSpace(cashierName))
        {
            return Transactions;
        }
        return Transactions.Where(t => string.Equals(t.CashierName, cashierName, StringComparison.OrdinalIgnoreCase));
    }

    public IEnumerable<Transaction> GetByDay(string cashierName, DateTime date)
    {
        if (string.IsNullOrWhiteSpace(cashierName))
        {
            return Transactions.Where(t => t.TimeStamp.Date == date.Date);
        }
        return Transactions.Where(t =>
                string.Equals(t.CashierName, cashierName, StringComparison.OrdinalIgnoreCase)
                && t.TimeStamp.Date == date.Date);
    }

    public void Save(string cashierName, int productId, string productName, double price, int beforeQuantity, int soldQuantity)
    {
        int transactionId = 0;
        if (Transactions is not null && Transactions.Count > 0)
        {
            transactionId = Transactions.Max(t => t.TransactionId) + 1;
        }
        else
        {
            transactionId = 1;
        }
        Transactions.Add(new Transaction()
        {
            TransactionId = transactionId,
            ProductId = productId,
            ProductName = productName,
            TimeStamp = DateTime.Now,
            Price = price,
            beforeQuantity = beforeQuantity,
            soldQuantity = soldQuantity,
            CashierName = cashierName
        });
    }

    public IEnumerable<Transaction> Search(string cashierName, DateTime startDate, DateTime endDate)
    {
        if (string.IsNullOrWhiteSpace(cashierName))
        {
            return Transactions.Where(t => t.TimeStamp.Date >= startDate.Date && t.TimeStamp <= endDate.AddDays(1).Date);
        }
        return Transactions.Where(t =>
                string.Equals(t.CashierName, cashierName, StringComparison.OrdinalIgnoreCase)
                && t.TimeStamp.Date >= startDate.Date && t.TimeStamp <= endDate.AddDays(1).Date);
    }
}