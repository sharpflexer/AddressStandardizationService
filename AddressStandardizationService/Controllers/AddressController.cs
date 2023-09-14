using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
namespace AddressStandardizationService.Controllers
{
    [Route("api/address")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IDadataService _dadataService;
        private readonly IMapper _mapper;
        private readonly ILogger<AddressController> _logger;


        public AddressController(IDadataService dadataService, IMapper mapper, ILogger<AddressController> logger)
        {
            _dadataService = dadataService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpPost("standardize")]
        public async Task<IActionResult> StandardizeAddress([FromBody] AddressRequestModel request)
        {
            try
            {
                _logger.LogInformation($"Received request for standardizing address: {request.RawAddress}");
                // Маппинг из модели запроса вашего приложения в модель запроса Dadata
                var dadataRequest = _mapper.Map<DadataRequestModel>(request);

                // Выполнение запроса к Dadata API
                var standardizedAddress = await _dadataService.StandardizeAddressAsync(dadataRequest);

                // Маппинг из модели ответа Dadata в модель ответа вашего приложения
                var responseModel = _mapper.Map<AddressResponseModel>(standardizedAddress);
                _logger.LogInformation($"Standardized address: {responseModel.StandardizedAddress}");
                return Ok(responseModel);
            }
            catch (Exception ex)
            {
                // Обработка ошибок
                _logger.LogError($"Error: {ex.Message}");
                return StatusCode(500, "Error");
            }
        }
    }

}
