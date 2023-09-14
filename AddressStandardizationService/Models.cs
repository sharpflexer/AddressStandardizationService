namespace AddressStandardizationService
{
    public class AddressRequestModel
    {
        public string RawAddress { get; set; }
    }

    public class AddressResponseModel
    {
        public string StandardizedAddress { get; set; }
    }

    // DadataRequestModel - представляет структуру запроса к Dadata API
    public class DadataRequestModel
    {
        public string RawAddress { get; set; }
    }

    // DadataResponseModel - представляет структуру ответа от Dadata API
    public class DadataResponseModel
    {
        public string StandardizedAddress { get; set; }
        // Другие поля из ответа Dadata API могут быть добавлены здесь
    }
}
