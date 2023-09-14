using System.Security.Cryptography;
using System.Text;

namespace AddressStandardizationService
{
    public interface IDadataService
    {
        Task<DadataResponseModel> StandardizeAddressAsync(DadataRequestModel rawAddress);

    }
    public class DadataService : IDadataService
    {
        private readonly HttpClient _httpClient;
        private readonly string _secretKey;

        public DadataService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _secretKey = configuration["DadataApiSettings:SecretKey"]; // Получите секретный ключ из конфигурации
        }

        public async Task<DadataResponseModel> StandardizeAddressAsync(DadataRequestModel requestModel)
        {
            try
            {
                // Преобразуйте объект запроса в JSON
                string requestBody = Newtonsoft.Json.JsonConvert.SerializeObject(new List<string>() { requestModel.RawAddress });

                // Создайте подпись запроса с использованием секретного ключа
                string signature = ComputeSignature(requestBody);

                // Отправьте POST-запрос к Dadata API
                var response = await _httpClient.PostAsync("", new StringContent(requestBody, Encoding.UTF8, "application/json"));

                // Прочитайте ответ Dadata API и извлеките стандартизированный адрес
                string responseContent = await response.Content.ReadAsStringAsync();
                // Здесь вы должны разобрать JSON-ответ и извлечь стандартизированный адрес
                Console.WriteLine(responseContent);

                // Проверьте успешность запроса
                if (response.IsSuccessStatusCode)
                {

                    // Верните стандартизированный адрес
                    return new DadataResponseModel()
                    {
                        StandardizedAddress = responseContent
                    };
                }
                else
                {
                    // Обработка ошибки, если запрос к Dadata API не успешен
                    return new DadataResponseModel()
                    {
                        StandardizedAddress = response.StatusCode.ToString()
                    }; 
                }
            }
            catch (Exception ex)
            {
                return new DadataResponseModel() { StandardizedAddress = ex.Message };
            }
        }

        // Метод для вычисления подписи запроса с использованием секретного ключа
        private string ComputeSignature(string data)
        {
            using (HMACSHA256 hmac = new HMACSHA256(Encoding.UTF8.GetBytes(_secretKey)))
            {
                byte[] hashBytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(data));
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
        }
    }

}
