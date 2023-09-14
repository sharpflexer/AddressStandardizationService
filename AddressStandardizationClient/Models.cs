using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressStandardizationClient
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
        public string Query { get; set; }
    }

    // DadataResponseModel - представляет структуру ответа от Dadata API
    public class DadataResponseModel
    {
        public string StandardizedAddress { get; set; }
        // Другие поля из ответа Dadata API могут быть добавлены здесь
    }
}
