
namespace CKO.Payments.DAL.Enums
{
    public class Transactions
    {
        public enum TransactionStatus
        {
            Pending = 1,
            Processing,
            Approved,
            Settled,
            Declined
        }
    }
}
