﻿using CKO.Payments.Data.DTO;
using Microsoft.EntityFrameworkCore;

namespace CKO.Payments.Data
{
    public class CkoContext : DbContext
    {
        public CkoContext(DbContextOptions<CkoContext> options) : base(options)
        {

        }

        #region DbSets

        public DbSet<Merchant> Merchants { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Card> Cards { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<LineItem> LineItems { get; set; }

        #endregion
    }
}