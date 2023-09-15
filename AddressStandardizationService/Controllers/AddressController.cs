using AddressStandardizationService.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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

        [HttpPost("full")]
        public async Task<IActionResult> StandardizeFullAddress([FromBody] string request)
        {
            try
            {
                return await StandardizeAddress<Address>(request);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("short")]
        public async Task<IActionResult> StandardizeShortAddress([FromBody] string request)
        {
            try
            {
                return await StandardizeAddress<ShortAddress>(request);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("geodata")]
        public async Task<IActionResult> StandardizeGeoData([FromBody] string request)
        {
            return await TryStandardizeAddress<GeoData>(request);
        }

        private async Task<IActionResult> TryStandardizeAddress<T>(string request)
        {
            try
            {
                return await StandardizeAddress<T>(request);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }

        private async Task<IActionResult> StandardizeAddress<T>(string request)
        {
            _logger.LogInformation($"Received request for standardizing address: {request}");
            var response = await _dadataService.StandardizeAddressAsync(request);
            string responseContent = await response.Content.ReadAsStringAsync();
            _logger.LogInformation($"Standardized address: {responseContent}");
            var addresses = JsonConvert.DeserializeObject<List<Address>>(responseContent);
            var mappedData = _mapper.Map<List<Address>, List<T>>(addresses);
            return CheckResponseStatus(response, mappedData);
        }


        private ObjectResult CheckResponseStatus(HttpResponseMessage response, object content)
        {
            if (response.IsSuccessStatusCode)
                return Ok(content);
            else
                return BadRequest(response.StatusCode.ToString());
        }

    }

}
