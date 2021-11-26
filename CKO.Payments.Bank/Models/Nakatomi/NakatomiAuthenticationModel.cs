
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
