using CSharpFunctionalExtensions;
using JSM.POCs.Communication.MessagePack.Price.Contracts;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace JSM.POCs.Communication.MessagePack.PriceOperations.Communication
{
    public class PricesClient
    {
        private readonly HttpClient _httpClient;
        private readonly MessagePackProxy _messagePackProxy;

        public PricesClient(HttpClient httpClient, MessagePackProxy messagePackProxy) =>
            (_httpClient, _messagePackProxy) = (httpClient, messagePackProxy);

        public async Task<List<PriceDto>> GetAllPrices() =>
            await _messagePackProxy.GetAsync<List<PriceDto>>(_httpClient, "/api/prices");

        public async Task<Result<ServerSuccess, ServerError>> SendPrices(PriceDto priceDto) =>
            await _messagePackProxy.PostAsync(_httpClient, "/api/prices", priceDto);
    }
}
