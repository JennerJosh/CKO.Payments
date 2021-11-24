using CKO.Payments.BL.Models;
using CKO.Payments.Data.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CKO.Payments.BL.Mappers
{
    internal static class CustomerMapper
    {
        public static Customer MapToCustomer(CustomerModel model)
        {
            return new()
            {
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Id = model.Id,
                Address = AddressMapper.MapToAddress(model.Address),
            };
        }

        public static CustomerModel MapToCustomerModel(Customer model)
        {
            return new()
            {
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Id = model.Id,
                Address = AddressMapper.MapToAddressModel(model.Address),
            };
        }
    }
}
