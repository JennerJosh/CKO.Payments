using CKO.Payments.BL.Models.Transactions;
using CKO.Payments.Data.DTO;

namespace CKO.Payments.BL.Mappers
{
    internal static class LineItemMapper
    {
        public static LineItem MapToLineItem(LineItemModel model)
        {
            return new()
            {
                Id = model.Id,
                Name = model.Name,
                Price = model.Price,
                Quantity = model.Quantity,
            };
        }

        public static LineItemModel MapToLineItemModel(LineItem model)
        {
            return new()
            {
                Id = model.Id,
                Name = model.Name,
                Price = model.Price,
                Quantity = model.Quantity,
            };
        }

        public static List<LineItem> MapToLineItems(List<LineItemModel> model)
        {
            return model
                .Select(x => MapToLineItem(x))
                .ToList();
        }

        public static List<LineItemModel> MapToLineItemModels(List<LineItem> model)
        {
            return model
                .Select(x => MapToLineItemModel(x))
                .ToList();
        }
    }
}
