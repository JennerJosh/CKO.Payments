using CKO.Payments.Bank.Extensions;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace CKO.Payments.Bank.Client
{
    public class HttpRequests : IHttpRequests
    {
        private readonly HttpClient _httpClient;

        public HttpRequests(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _httpClient.Timeout = TimeSpan.FromMinutes(5);
        }

        public void SetBaseAddress(string baseUrl) => _httpClient.BaseAddress = new Uri(baseUrl);

        /// <summary>
        /// Build the request that can be executed
        /// </summary>
        /// <param name="endpoint">Endpoint to be called</param>
        /// <param name="method">Http method to send the request as</param>
        /// <param name="body">Object to pass with the request</param>
        public HttpRequestMessage BuildRequest(string endpoint, HttpMethod method, object body = null)
        {
            var httpRequest = new HttpRequestMessage
            {
                Method = method,
                RequestUri = new Uri($"{_httpClient.BaseAddress.AbsoluteUri}/{endpoint}"),
            };

            if (body != null)
                AddJsonBody(httpRequest, body);

            return httpRequest;
        }

        /// <summary>
        /// Build a request that can be executed
        /// </summary>
        /// <param name="endpoint">Endpoint to be called</param>
        /// <param name="method">Http method to send the request as</param>
        /// <param name="json">Json string to send with the request</param>
        /// <returns></returns>
        public HttpRequestMessage BuildRequest(string endpoint, HttpMethod method, string json)
        {
            var httpRequest = new HttpRequestMessage
            {
                Method = method,
                RequestUri = new Uri($"{_httpClient.BaseAddress.AbsoluteUri}/{endpoint}"),
            };

            if (!string.IsNullOrEmpty(json))
                AddJsonBody(httpRequest, json);

            return httpRequest;
        }

        /// <summary>
        /// Execute the request and transform response into provided type
        /// </summary>
        /// <typeparam name="T">Type of response you are expected from request</typeparam>
        /// <returns>returns back response from request</returns>
        public async Task<T> ExecuteRequest<T>(HttpRequestMessage request) where T : new()
        {
            var response = await _httpClient.SendAsync(request);

            if (!response.IsSuccessStatusCode || response.Content == null)
                throw new Exception(await response.Content?.ReadAsStringAsync() ?? "Emtpy Content");

            var stringContent = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<T>(stringContent);

            return result;
        }

        /// <summary>
        /// Execute http request
        /// </summary>
        /// <param name="request">Request to be executed</param>
        /// <returns>Response from the request</returns>
        public async Task<HttpResponseMessage> ExecuteRequest(HttpRequestMessage request)
        {
            return await _httpClient.SendAsync(request);
        }

        private void AddJsonBody(HttpRequestMessage request, object body)
        {
            var json = body.Serialize(ignoreNullValues: true);
            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            request.Content = stringContent;
        }

        private void AddJsonBody(HttpRequestMessage request, string json)
        {
            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            request.Content = stringContent;
        }
    }
}
