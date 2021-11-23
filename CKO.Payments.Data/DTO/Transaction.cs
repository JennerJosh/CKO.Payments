using System.ComponentModel.DataAnnotations.Schema;

namespace CKO.Payments.Data.DTO
{
    [Table("Transactions")]
    public class Transaction
    {
        public Guid Id { get; set; }

        public Guid MerchantId { get; set; }

        public Guid CustomerId { get; set; }

        public decimal Amount { get; set; }

        public ICollection<LineItem> LineItems { get; set; }

        public DateTime CreatedDate { get; set; }

    }
}
