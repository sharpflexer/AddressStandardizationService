using System.Security.Cryptography;
using System.Text;

namespace AddressStandardizationService
{
    public interface IDadataService
    {
        Task<HttpResponseMessage> StandardizeAddressAsync(string rawAddress);
    }
    public class DadataService : IDadataService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<DadataService> _logger;

        public DadataService(HttpClient httpClient, ILogger<DadataService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<HttpResponseMessage> StandardizeAddressAsync(string rawAddress)
        {
            string requestBody = Newtonsoft.Json.JsonConvert.SerializeObject(new List<string> {rawAddress });
            var response = await _httpClient.PostAsync("", new StringContent(requestBody, Encoding.UTF8, "application/json"));
            return response;
        }
    }

}
