
namespace CKO.Payments.Bank.Client
{
    public interface IHttpRequests
    {
        HttpRequestMessage BuildRequest(string endpoint, HttpMethod method, object body = null);
        HttpRequestMessage BuildRequest(string endpoint, HttpMethod method, string json);
        Task<HttpResponseMessage> ExecuteRequest(HttpRequestMessage request);
        Task<T> ExecuteRequest<T>(HttpRequestMessage request) where T : new();
        void SetBaseAddress(string baseUrl);
    }
}