using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AddressStandardizationService.Controllers
{
    [Route("api/address")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IDadataService _dadataService;
        private readonly IMapper _mapper;

        public AddressController(IDadataService dadataService, IMapper mapper)
        {
            _dadataService = dadataService;
            _mapper = mapper;
        }

        [HttpPost("standardize")]
        public async Task<IActionResult> StandardizeAddress([FromBody] AddressRequestModel request)
        {
            try
            {
                // Маппинг из модели запроса вашего приложения в модель запроса Dadata
                var dadataRequest = _mapper.Map<DadataRequestModel>(request);

                // Выполнение запроса к Dadata API
                string standardizedAddress = await _dadataService.StandardizeAddressAsync(dadataRequest.Query);

                // Маппинг из модели ответа Dadata в модель ответа вашего приложения
                var responseModel = _mapper.Map<AddressResponseModel>(standardizedAddress);

                return Ok(responseModel);
            }
            catch (Exception ex)
            {
                // Обработка ошибок
                Console.WriteLine(ex.Message);
                return StatusCode(500, "Error");
            }
        }
    }

}
