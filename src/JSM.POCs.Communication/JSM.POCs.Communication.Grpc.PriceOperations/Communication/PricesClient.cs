using CSharpFunctionalExtensions;
using JSM.POCs.Communication.Grpc.PricesService.Protos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JSM.POCs.Communication.Grpc.PriceOperations.Communication
{
    public class PricesClient
    {
        private readonly GrpcProxy _gRpcProxy;

        public PricesClient(GrpcProxy messagePackProxy) => (_gRpcProxy) = (messagePackProxy);

        public async Task<List<PriceModel>> GetAllPrices() => await _gRpcProxy.GetAsync();

        public async Task<Result<ServerSuccess, ServerError>> SendPrices(PriceModel price) => await _gRpcProxy.PostAsync(price);
    }
}
