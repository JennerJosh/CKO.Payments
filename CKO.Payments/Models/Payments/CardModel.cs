using CKO.Payments.BL.Models;
using System;
using System.Text.RegularExpressions;

namespace CKO.Payments.Models.Payments
{
    public class PaymentCard
    {
        public string Name { get; set; }
        public string Number { get; set; }
        public int ExpiryMonth { get; set; }
        public int ExpiryYear { get; set; }
        public int Cvv { get; set; }

        public CardModel GetCardModel()
        {
            return new()
            {
                Name = Name,
                Number = Number,
                ExpiryMonth = ExpiryMonth,
                ExpiryYear = ExpiryYear,
                Cvv = Cvv
            };
        }


    }
}
