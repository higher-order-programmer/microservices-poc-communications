using JSM.POCs.Communication.MessagePack.PriceOperations.Communication;
using JSM.POCs.Communication.MessagePack.Price.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace JSM.POCs.Communication.MessagePack.PriceOperations.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
	public class PricesOperationsController: ControllerBase
    {
        private readonly PricesClient _pricesClient;

        public PricesOperationsController(PricesClient pricesClient) => (_pricesClient) = (pricesClient);

        [HttpGet]
        [Route("prices")]
        public async Task<IActionResult> GetPrices() =>
            Ok(await _pricesClient.GetAllPrices());

        [HttpPost]
        [Route("prices")]
        public async Task<IActionResult> AddPrice(PriceDto priceRequest)
        {
            var result = await _pricesClient.SendPrices(priceRequest);

            return result.IsSuccess
                                ? Ok()
                                : result.Error.StatusCode == StatusCodes.Status400BadRequest
                                    ? BadRequest(result.Error.Message)
                                    : StatusCode(StatusCodes.Status500InternalServerError, result.Error.Message);
        }

    }
}
