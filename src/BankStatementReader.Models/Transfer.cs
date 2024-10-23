namespace BankStatementReader.Models
{
    public class Transfer
    {
        public DateTime Date { get; set; }

        public DateTime ExpiryDate { get; set; }

        public required string Type { get; set; }

        public required string Details { get; set; }

        public decimal Value { get; set; }

        public TransferDirection Direction => Value > 0 ? TransferDirection.Income : TransferDirection.Expense;
    }
}