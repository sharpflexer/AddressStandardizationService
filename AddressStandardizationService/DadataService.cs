namespace AddressStandardizationService
{
    public interface IDadataService
    {
        Task<string> StandardizeAddressAsync(string rawAddress);
    }

    public class DadataService : IDadataService
    {
        private readonly HttpClient _httpClient;

        public DadataService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> StandardizeAddressAsync(string rawAddress)
        {
            try
            {
                // Создайте объект, который будет отправлен в Dadata API
                var requestObject = new
                {
                    query = rawAddress // Передайте сырой адрес для стандартизации
                };

                // Преобразуйте объект запроса в JSON
                string requestBody = Newtonsoft.Json.JsonConvert.SerializeObject(requestObject);

                // Отправьте POST-запрос к Dadata API
                var response = await _httpClient.PostAsync("", new StringContent(requestBody));

                // Проверьте успешность запроса
                if (response.IsSuccessStatusCode)
                {
                    // Прочитайте ответ Dadata API и извлеките стандартизированный адрес
                    string responseContent = await response.Content.ReadAsStringAsync();
                    // Здесь вы должны разобрать JSON-ответ и извлечь стандартизированный адрес

                    // Верните стандартизированный адрес
                    return responseContent;
                }
                else
                {
                    // Обработка ошибки, если запрос к Dadata API не успешен
                    return null;
                }
            }
            catch (Exception ex)
            {
                // Обработка и логирование ошибок
                return null;
            }
        }
    }
}
