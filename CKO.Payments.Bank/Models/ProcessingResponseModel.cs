﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CKO.Payments.Bank.Models
{
    public class BankResponseModel
    {
        public bool IsSuccess { get; set; }
        public string PaymentId { get; set; }
        public string Message { get; set; }
    }
}
