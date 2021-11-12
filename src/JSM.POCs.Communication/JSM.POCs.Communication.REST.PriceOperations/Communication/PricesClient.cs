using CSharpFunctionalExtensions;
using JSM.POCs.Communication.REST.PriceAPI;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace JSM.POCs.Communication.REST.PriceOperations.Communication
{
    public class PricesClient
    {
        private readonly HttpClient _httpClient;
        private readonly RestProxy _restProxy;

        public PricesClient(HttpClient httpClient, RestProxy messagePackProxy) =>
            (_httpClient, _restProxy) = (httpClient, messagePackProxy);

        public async Task<List<PriceDto>> GetAllPrices() =>
            await _restProxy.GetAsync<List<PriceDto>>(_httpClient, "/api/prices");

        public async Task<Result<ServerSuccess, ServerError>> SendPrices(PriceDto priceDto) =>
            await _restProxy.PostAsync(_httpClient, "/api/prices", priceDto);
    }
}
