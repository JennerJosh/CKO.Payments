using CKO.Payments.Data.DTO;
using System;
using System.Collections.Generic;
using static CKO.Payments.DAL.Enums.Transactions;

namespace CKO.Payments.Tests.Mock
{
    public class MerchantsMockedData
    {
        public List<Merchant> Merchants
        {
            get
            {
                var mockedData = new List<Merchant>();

                // Set Mocked Records

                mockedData.Add(new Merchant() { 
                    Name = "Test",
                    MerchantSecret = "SuperSecureSecret",
                    Email = "test@test.com",
                    Id = Guid.Parse("7bc7cd41-7ab0-4009-aba6-f75799161cc7")
                });

                mockedData.Add(new Merchant()
                {
                    Name = "Amazon",
                    MerchantSecret = "ABC123",
                    Email = "test2@test2.com",
                    Id = Guid.Parse("45d0e850-929a-41da-abb1-463da8bb50d2")
                });

                mockedData.Add(new Merchant()
                {
                    Name = "Bobs Burgers",
                    MerchantSecret = "RandomString",
                    Email = "test3@test3.com",
                    Id = Guid.Parse("046b2260-c212-47cd-8689-ead129f66c1a")
                });

                mockedData.Add(new Merchant()
                {
                    Name = "Some Restaurant",
                    MerchantSecret = "ThisIsATest",
                    Email = "test4@test4.com",
                    Id = Guid.Parse("d7534e0e-0de7-4de5-959d-2ed349780d3c")
                });

                return mockedData;
            }
        }


    }
}
