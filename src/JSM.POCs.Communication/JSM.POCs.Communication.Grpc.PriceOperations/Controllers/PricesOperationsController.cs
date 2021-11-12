using JSM.POCs.Communication.Grpc.PriceOperations.Communication;
using JSM.POCs.Communication.Grpc.PricesService.Protos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace JSM.POCs.Communication.Grpc.PriceOperations.Controllers
{
    [ApiController]
    [Route("api/[controller]/prices")]
    public class PricesOperationsController : ControllerBase
    {
        private readonly PricesClient _pricesClient;

        public PricesOperationsController(PricesClient pricesClient) => (_pricesClient) = (pricesClient);

        [HttpGet]
        public async Task<IActionResult> GetPrices() => Ok(await _pricesClient.GetAllPrices());

        [HttpPost]
        public async Task<IActionResult> AddPrice(PriceModel price)
        {
            var result = await _pricesClient.SendPrices(price);

            return result.IsSuccess
                                ? Ok()
                                : result.Error.StatusCode == StatusCodes.Status400BadRequest
                                    ? BadRequest(result.Error.Message)
                                    : StatusCode(StatusCodes.Status500InternalServerError, result.Error.Message);
        }
    }
}
