using System;
namespace CoreBusiness;
public class Transaction
{
    public int TransactionId { get; set; }
    public DateTime TimeStamp { get; set; }
    public int ProductId { get; set; }
    public string ProductName { get; set; }
    public double Price { get; set; }
    public int beforeQuantity { get; set; }
    public int soldQuantity { get; set; }
    public string CashierName { get; set; }
}