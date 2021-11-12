using Grpc.Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JSM.POCs.Communication.Grpc.PricesService.Services
{
    public class PriceService: PriceProtoService.PriceProtoServiceBase
    {
        private readonly List<PriceModel> _prices = new List<PriceModel>();

        public override Task<PriceResponse> GetAll(Empty empty, ServerCallContext context)
        {
            var response = new PriceResponse();
            response.Prices.AddRange(_prices);

            return Task.FromResult(response);
        }

        public override Task<Empty> Insert(PriceModel price, ServerCallContext context)
        {
            if (_prices.Count <= 200)
                _prices.Add(price);

            return Task.FromResult(new Empty());
        }
    }
}
