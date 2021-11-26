using CKO.Payments.Data.DTO;
using System;
using System.Collections.Generic;
using static CKO.Payments.DAL.Enums.Transactions;

namespace CKO.Payments.Tests.Mock
{
    public class TransactionsMockedData
    {
        public List<Transaction> Transactions
        {
            get
            {
                var mockedData = new List<Transaction>();

                // Set Mocked Records

                mockedData.Add(new Transaction()
                {
                    Amount = 123.00M,
                    BankPaymentId = String.Empty,
                    Card = null,
                    CreatedDate = DateTime.Now,
                    Currency = "GBP",
                    Customer = null,
                    Id = Guid.Parse("ef0ab4d3-ba2e-4be0-8a22-8bc10f824150"),
                    LineItems = null,
                    Status = (int)TransactionStatus.Pending,
                    StatusMessage = "Payment Intent Created",
                    MerchantId = Guid.Parse("7bc7cd41-7ab0-4009-aba6-f75799161cc7")

                });

                mockedData.Add(new Transaction()
                {
                    Amount = 13.00M,
                    BankPaymentId = String.Empty,
                    Card = null,
                    CreatedDate = DateTime.Now.AddHours(-1),
                    Currency = "EUR",
                    Customer = null,
                    Id = Guid.Parse("c60bd10b-0e20-4249-9b26-ce4a75a0ab15"),
                    LineItems = null,
                    Status = (int)TransactionStatus.Pending,
                    StatusMessage = "Payment Intent Created",
                    MerchantId = Guid.Parse("45d0e850-929a-41da-abb1-463da8bb50d2")

                });

                mockedData.Add(new Transaction()
                {
                    Amount = 1233.00M,
                    BankPaymentId = "bcf1b67a-5a73-4157-bdb1-d522e5342880",
                    Card = new Card()
                    {
                        ExpiryMonth = "2",
                        ExpiryYear = "2023",
                        Number = "1234 5678 1234 5678",
                        Name = "John Jones",
                        TransactionId = Guid.Parse("093dda5f-439f-4825-82bf-4bdd267910cb"),
                        Id = Guid.Parse("111feea0-eba7-4e30-b4bd-f9fab5fb4ce7")
                    },
                    CreatedDate = DateTime.Now.AddHours(-3),
                    Currency = "GBP",
                    CustomerId = Guid.Parse("1d2455d7-05e9-4502-9fb6-54830114ee66"),
                    Customer = new Customer()
                    {
                        Email = "john@jones.com",
                        FirstName = "John",
                        LastName = "Jones",
                        Id = Guid.Parse("1d2455d7-05e9-4502-9fb6-54830114ee66"),
                        Address = new Address()
                        {
                            Line1 = "1",
                            Line2 = "2",
                            Line3 = "3",
                            Town = "4",
                            County = "5",
                            Country = "6",
                            PostCode = "7",
                            CustomerId = Guid.Parse("1d2455d7-05e9-4502-9fb6-54830114ee66"),
                            Id = Guid.Parse("605b51a6-d52f-486f-a8e2-126230f471fe")
                        }
                    },
                    Id = Guid.Parse("093dda5f-439f-4825-82bf-4bdd267910cb"),
                    LineItems = new List<LineItem>() {
                        new LineItem()
                        {
                            Id = Guid.NewGuid(),
                            Name = "Some Product",
                            Price = 616.5M,
                            Quantity = 2,
                            TransactionId = Guid.Parse("093dda5f-439f-4825-82bf-4bdd267910cb")
                        }
                    },
                    Status = (int)TransactionStatus.Approved,
                    StatusMessage = "Payment ready for settlement",
                    MerchantId = Guid.Parse("7bc7cd41-7ab0-4009-aba6-f75799161cc7")

                });

                return mockedData;
            }
        }


    }
}
