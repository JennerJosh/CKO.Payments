using CKO.Payments.BL.Extensions;

namespace CKO.Payments.BL.Models.Transactions
{
    public class LineItemModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public bool IsValid()
        {
            return !string.IsNullOrEmpty(Name)
                && Price > decimal.Zero
                && Quantity > int.MinValue;
        }

        public void MaskSensitiveData()
        {
            Name = Name.Mask();
        }
    }
}
