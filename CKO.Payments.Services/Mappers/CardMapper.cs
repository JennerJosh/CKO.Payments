using CKO.Payments.BL.Models;
using CKO.Payments.Data.DTO;

namespace CKO.Payments.BL.Mappers
{
    internal static class CardMapper
    {
        public static Card MapToCard(CardModel model)
        {
            return new()
            {
                Id = model.Id,
                ExpiryMonth = model.ExpiryMonth.ToString(),
                ExpiryYear = model.ExpiryYear.ToString(),
                Name = model.Name,
                Number = model.Number,
            };
        }

        public static CardModel MapToCardModel(Card model)
        {
            return new()
            {
                Id = model.Id,
                ExpiryMonth = Convert.ToInt32(model.ExpiryMonth),
                ExpiryYear = Convert.ToInt32(model.ExpiryYear),
                Name = model.Name,
                Number = model.Number,
            };
        }
    }
}
