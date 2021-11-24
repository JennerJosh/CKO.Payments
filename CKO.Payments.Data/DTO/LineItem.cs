using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CKO.Payments.Data.DTO
{
    [Table("LineItems")]
    public class LineItem
    {
        public Guid Id { get; set; }

        public Guid TransactionId { get; set; }

        [MaxLength(255)]
        public string Name { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public virtual Transaction Transaction { get; set; }
    }
}
