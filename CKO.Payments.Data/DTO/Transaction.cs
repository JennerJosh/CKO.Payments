using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CKO.Payments.Data.DTO
{
    [Table("Transactions")]
    public class Transaction
    {
        public Guid Id { get; set; }

        public Guid MerchantId { get; set; }

        public Guid? CustomerId { get; set; }

        public string? BankPaymentId { get; set; }

        public decimal Amount { get; set; }

        [MaxLength(3)]
        public string Currency { get; set; }

        public int Status { get; set; }

        public string? StatusMessage { get; set; }

        public DateTime CreatedDate { get; set; }

        public virtual Customer? Customer { get; set; }

        public virtual Card? Card { get; set; }

        public virtual Merchant Merchant { get; set; }

        public ICollection<LineItem>? LineItems { get; set; }


    }
}
