using CKO.Payments.BL.Models;
using CKO.Payments.Data.DTO;

namespace CKO.Payments.BL.Mappers
{
    internal static class AddressMapper
    {
        public static Address MapToAddress(AddressModel model)
        {
            return new()
            {
                Id = model.Id,
                Country = model.Country,
                County = model.County,
                Line1 = model.Line1,
                Line2 = model.Line2,
                Line3 = model.Line3,
                PostCode = model.PostCode,
                Town = model.Town,
            };
        }

        public static AddressModel MapToAddressModel(Address model)
        {
            return new()
            {
                Id = model.Id,
                Country = model.Country,
                County = model.County,
                Line1 = model.Line1,
                Line2 = model.Line2,
                Line3 = model.Line3,
                PostCode = model.PostCode,
                Town = model.Town,
            };
        }
    }
}
