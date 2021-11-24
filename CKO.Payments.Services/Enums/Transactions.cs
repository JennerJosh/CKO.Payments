
namespace CKO.Payments.BL.Enums
{
    internal class Transactions
    {
        public enum TransactionStatus
        {
            Pending = 1,
            Processing,
            Settled,
            Failed
        }
    }
}
