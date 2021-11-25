using static CKO.Payments.DAL.Enums.Transactions;

namespace CKO.Payments.BL.Models
{
    public class TransactionModel
    {
        private const int CURRENCY_CODE_LENGTH = 3;

        public Guid Id { get; set; }
        public Guid MerchantId { get; set; }
        public string BankPaymentId { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public int Status { get; set; } = (int)TransactionStatus.Pending;
        public string StatusMessage { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public CustomerModel Customer { get; set; }
        public CardModel Card { get; set; }
        public MerchantModel Merchant { get; set; }
        public List<LineItemModel> LineItems { get; set; }

        public TransactionModel()
        {

        }

        public TransactionModel(Guid merchantId, decimal amount, string currency)
        {
            MerchantId = merchantId;
            Amount = amount;
            Currency = currency;
        }

        public TransactionModel(decimal amount, string currency)
        {
            Amount = amount;
            Currency = currency;
        }

        public bool IsValid()
        {
            return Id != Guid.Empty
                && IsCurrencyValid()
                && AreItemsValid()
                && Customer.IsValid()
                && Card.IsValid()
                && IsAmountValid();
        }

        public bool IsStubValid()
        {
            return IsAmountSet() && IsCurrencyValid();
        }

        public void SetStatus(int status, string statusMessage)
        {
            Status = status;
            StatusMessage = statusMessage;
        }

        public void SetPaymentId(string bankPaymentId)
        {
            BankPaymentId = bankPaymentId;
        }

        private bool IsAmountSet()
        {
            return Amount > decimal.Zero;
        }

        private bool IsCurrencyValid()
        {
            if (string.IsNullOrEmpty(Currency) || Currency.Length != CURRENCY_CODE_LENGTH)
                return false;

            return true;
        }

        private bool AreItemsValid()
        {
            return LineItems.All(x => x.IsValid());
        }

        private bool IsAmountValid()
        {
            return LineItems.Sum(x => x.Price * x.Quantity) == Amount;
        }

    }
}
