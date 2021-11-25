using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CKO.Payments.Bank.Models.Nakatomi
{
    internal class NakatomiAuthenticationModel
    {
        public string User { get; set; }
        public string Secret { get; set; }

        public NakatomiAuthenticationModel(string user, string secret)
        {
            User = user;
            Secret = secret;
        }
    }
}
