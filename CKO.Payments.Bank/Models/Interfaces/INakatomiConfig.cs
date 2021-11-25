namespace CKO.Payments.Bank.Models.Interfaces
{
    public interface INakatomiConfig
    {
        string BaseUrl { get; set; }
        string UserName { get; set; }
        string Password { get; set; }
    }
}