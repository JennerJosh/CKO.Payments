using CKO.Payments.BL.Models.Transactions;

namespace CKO.Payments.Models.Payments
{
    public class PaymentLineItemModel
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public LineItemModel GetLineItemModel()
        {
            return new()
            {
                Name = Name,
                Price = Price,
                Quantity = Quantity
            };
        }

    }
}
