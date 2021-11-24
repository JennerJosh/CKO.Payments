using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CKO.Payments.BL.Models
{
    public class CardModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Number { get; set; }

        public string ExpiryMonth { get; set; }

        public string ExpiryYear { get; set; }
    }
}
