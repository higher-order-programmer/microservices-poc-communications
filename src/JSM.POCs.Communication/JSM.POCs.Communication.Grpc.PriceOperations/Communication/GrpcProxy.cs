using CSharpFunctionalExtensions;
using Grpc.Net.Client;
using JSM.POCs.Communication.Grpc.PricesService.Protos;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JSM.POCs.Communication.Grpc.PriceOperations.Communication
{
    public record ServerSuccess();
    public record ServerError(int StatusCode, string Message);

    public class GrpcProxy
    {
        private const string ContentType = "application/json";
        private readonly GrpcChannel _channel;

        public GrpcProxy() => _channel = GrpcChannel.ForAddress("https://localhost:5005");

        public async Task<List<PriceModel>> GetAsync()
        {
            var client = new PriceProtoService.PriceProtoServiceClient(_channel);
            var reply = await client.GetAllAsync(new Empty());

            return reply.Prices.ToList();
        }

        public async Task<Result<ServerSuccess, ServerError>> PostAsync(PriceModel price)
        {
            var client = new PriceProtoService.PriceProtoServiceClient(_channel);
            var reply = await client.InsertAsync(price);

            return Result.Success<ServerSuccess, ServerError>(new ServerSuccess());
        }
    }
}
