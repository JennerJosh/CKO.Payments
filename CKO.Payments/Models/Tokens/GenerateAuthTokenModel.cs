namespace CKO.Payments.Models.Tokens
{
    public class GenerateAuthTokenModel
    {
        public string Email { get; set; }   
        public string Secret { get; set; }
    }
}
