using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CKO.Payments.Data.DTO
{
    [Table("Cards")]
    public class Card
    {
        public Guid Id { get; set; }

        public Guid TransactionId { get; set; }

        [MaxLength(512)]
        public string Name { get; set; }

        [MaxLength(512)]
        public string Number { get; set; }

        [MaxLength(512)]
        public string ExpiryMonth { get; set; }

        [MaxLength(512)]
        public string ExpiryYear { get; set; }

        public virtual Transaction Transaction { get; set; }
    }
}
