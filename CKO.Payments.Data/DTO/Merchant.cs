using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CKO.Payments.Data.DTO
{
    [Table("Merchants")]
    public partial class Merchant
    {
        public Guid Id { get; set; }

        [MaxLength(255)]
        public string Name { get; set; }

        [MaxLength (320)]
        public string Email { get; set; }

        [MaxLength(512)]
        public string MerchantSecret { get; set; }

        public virtual ICollection<Transaction> Transactions { get;}
    }
}
