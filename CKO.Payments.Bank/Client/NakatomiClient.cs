using CKO.Payments.Bank.Client.Interface;
using CKO.Payments.Bank.Exceptions;
using CKO.Payments.Bank.Extensions;
using CKO.Payments.Bank.Models;
using CKO.Payments.Bank.Models.Interfaces;
using CKO.Payments.Bank.Models.Nakatomi;
using System.Net;
using System.Net.Http.Headers;

namespace CKO.Payments.Bank.Client
{
    public class NakatomiClient : IBankClient
    {
        private readonly IHttpRequests _httpRequests;
        private readonly INakatomiConfig _nakatomiConfig;

        private static string token { get; set; } = String.Empty;

        public NakatomiClient(INakatomiConfig nakatomiConfig, IHttpRequests httpRequests)
        {
            _nakatomiConfig = nakatomiConfig;
            _httpRequests = httpRequests;

            _httpRequests.SetBaseAddress(_nakatomiConfig.BaseUrl);
        }

        public async Task<ProcessingResponseModel> ProcessPaymentAsync(PaymentProcessingModel model)
        {
            var endpoint = "transactions/process";

            var request = _httpRequests.BuildRequest(endpoint, HttpMethod.Post, model);
            AddAuthenticationHeader(request);

            var response = await _httpRequests.ExecuteRequest(request);

            return await ProcessResponse(response, async () => await ProcessPaymentAsync(model));
        }

        public async Task<SettlementResponseModel> SettlePaymentAsync(PaymentSettlementModel model)
        {
            var endpoint = "transactions/settle";

            var request = _httpRequests.BuildRequest(endpoint, HttpMethod.Post, model);
            AddAuthenticationHeader(request);

            var response = await _httpRequests.ExecuteRequest(request);

            return await ProcessResponse(response, async () => await SettlePaymentAsync(model));
        }

        private async Task Authenticate()
        {
            var endpoint = "token/generate";
            var authModel = new NakatomiAuthenticationModel(_nakatomiConfig.UserName, _nakatomiConfig.Password);

            var request = _httpRequests.BuildRequest(endpoint, HttpMethod.Post, authModel);
            var response = await _httpRequests.ExecuteRequest(request);
            var content = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                token = content.Replace("\"", "");
            }
            else
            {
                throw new AuthorizationException(content);
            }

        }

        private void AddAuthenticationHeader(HttpRequestMessage request)
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        private async Task<T> ProcessResponse<T>(HttpResponseMessage response, Func<Task<T>> action)
        {
            var content = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                return content.Deserialize<T>();
            }

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                await Authenticate();
                return await action();
            }

            throw new BankRequestException(content);

        }
    }
}
