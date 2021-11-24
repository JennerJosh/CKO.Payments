﻿using CKO.Payments.BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CKO.Payments.BL.Services.Interfaces
{
    public interface IMerchantsService
    {
        MerchantModel RegisterMerchant(MerchantModel merchant);
        MerchantModel GetMerchantFromEmail(string email);
    }
}