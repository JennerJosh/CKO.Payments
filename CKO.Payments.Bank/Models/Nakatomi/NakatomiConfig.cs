using CKO.Payments.Bank.Models.Interfaces;

namespace CKO.Payments.Bank.Models.Nakatomi
{
    public class NakatomiConfig : INakatomiConfig
    {
        public string BaseUrl { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
