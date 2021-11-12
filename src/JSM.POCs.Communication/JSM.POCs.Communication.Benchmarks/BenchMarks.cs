using BenchmarkDotNet.Attributes;
using System;
using RandomDataGenerator.Randomizers;
using RandomDataGenerator.FieldOptions;
using System.Collections.Generic;
using System.Net.Http;

using RestConsumerAPIController = JSM.POCs.Communication.REST.PriceOperations.Controllers.PricesOperationsController;
using RestClientCommunication = JSM.POCs.Communication.REST.PriceOperations.Communication;
using RestApi = JSM.POCs.Communication.REST.PriceAPI;

using MsgPackConsumerAPIController = JSM.POCs.Communication.MessagePack.PriceOperations.Controllers.PricesOperationsController;
using MsgPackClientCommunication = JSM.POCs.Communication.MessagePack.PriceOperations.Communication;
using MsgPackContracts = JSM.POCs.Communication.MessagePack.Price.Contracts;

using GrpcConsumerAPIController = JSM.POCs.Communication.Grpc.PriceOperations.Controllers.PricesOperationsController;
using GrpcClientCommunication = JSM.POCs.Communication.Grpc.PriceOperations.Communication;
using GrpcService = JSM.POCs.Communication.Grpc.PricesService;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace JSM.POCs.Communication.Benchmarks
{
    [MemoryDiagnoser]
    public class BenchMarks: ControllerBase
    {
        private readonly RestConsumerAPIController _restConsumerAPIController;
        private readonly MsgPackConsumerAPIController _msgPackConsumerAPIController;
        private readonly GrpcConsumerAPIController _gRpcConsumerAPIController;

        private readonly IRandomizerString _randomizerText = RandomizerFactory.GetRandomizer(new FieldOptionsText {Min = 50, Max = 50 });
        private readonly IRandomizerNumber<int> _randomizerNumber = RandomizerFactory.GetRandomizer(new FieldOptionsInteger());

        public BenchMarks()
        {
            var restProxy = new RestClientCommunication.RestProxy();
            var restPricesClient = new RestClientCommunication.PricesClient(new HttpClient { BaseAddress = new Uri("https://localhost:5001") }, restProxy );
            _restConsumerAPIController = new RestConsumerAPIController(restPricesClient);

            var msgPackProxy = new MsgPackClientCommunication.MessagePackProxy();
            var msgPackPricesClient = new MsgPackClientCommunication.PricesClient(new HttpClient { BaseAddress = new Uri("https://localhost:5003") }, msgPackProxy);
            _msgPackConsumerAPIController = new MsgPackConsumerAPIController(msgPackPricesClient);

            var gRpcProxy = new GrpcClientCommunication.GrpcProxy();
            var gRpcPricesClient = new GrpcClientCommunication.PricesClient(gRpcProxy);
            _gRpcConsumerAPIController = new GrpcConsumerAPIController(gRpcPricesClient);
        }

        [GlobalSetup]
        public void Setup()
        {
        }

        [Benchmark(Baseline = true)]
        public async Task<IActionResult> RestCommunication()
        {
            var price = new RestApi.PriceDto
            {
                CenterCode = _randomizerText.Generate(),
                ClientIssuer = _randomizerText.Generate(),
                CodePaymentTerm = _randomizerText.Generate(),
                CustomerCode = _randomizerText.Generate(),
                DistributionChannel = _randomizerText.Generate(),
                Incoterm = _randomizerText.Generate(),
                IssuerName = _randomizerText.Generate(),
                OrderId = _randomizerText.Generate(),
                OrderType = _randomizerText.Generate(),
                OrganizationCode = _randomizerText.Generate(),
                PaymentForm = _randomizerText.Generate(),
                PostalCode = _randomizerText.Generate(),
                Products = new List<RestApi.ProductDto>()
                /*
                Products = new List<RestApi.ProductDto>
                {
                    new RestApi.ProductDto
                    {
                        id = (int)_randomizerNumber.Generate(),
                        name = _randomizerText.Generate(),
                        brand = _randomizerText.Generate(),
                        model = _randomizerText.Generate()
                    }
                }
                */
            };

            for (var i = 1; i <= 10; i++)
            {
                var product = new RestApi.ProductDto
                {
                    id = (int)_randomizerNumber.Generate(),
                    name = _randomizerText.Generate(),
                    brand = _randomizerText.Generate(),
                    model = _randomizerText.Generate()
                };
                price.Products.Add(product);
            }

            await _restConsumerAPIController.AddPrice(price);
            return Ok(await _restConsumerAPIController.GetPrices());
        }

        [Benchmark]
        public async Task<IActionResult> MessagePackCommunication()
        {
            var price = new MsgPackContracts.PriceDto
            {
                CenterCode = _randomizerText.Generate(),
                ClientIssuer = _randomizerText.Generate(),
                CodePaymentTerm = _randomizerText.Generate(),
                CustomerCode = _randomizerText.Generate(),
                DistributionChannel = _randomizerText.Generate(),
                Incoterm = _randomizerText.Generate(),
                IssuerName = _randomizerText.Generate(),
                OrderId = _randomizerText.Generate(),
                OrderType = _randomizerText.Generate(),
                OrganizationCode = _randomizerText.Generate(),
                PaymentForm = _randomizerText.Generate(),
                PostalCode = _randomizerText.Generate(),
                Products = new List<MsgPackContracts.ProductDto>()
                /*
                Products = new List<MsgPackContracts.ProductDto>
                {
                    new MsgPackContracts.ProductDto
                    {
                        id = (int) _randomizerNumber.Generate(),
                        name = _randomizerText.Generate(),
                        brand = _randomizerText.Generate(),
                        model = _randomizerText.Generate()
                    }
                }
                */
            };

            for (var i = 1; i <= 10; i++)
            {
                var product = new MsgPackContracts.ProductDto
                {
                    id = (int)_randomizerNumber.Generate(),
                    name = _randomizerText.Generate(),
                    brand = _randomizerText.Generate(),
                    model = _randomizerText.Generate()
                };
                price.Products.Add(product);
            }

            await _msgPackConsumerAPIController.AddPrice(price);
            return Ok(await _msgPackConsumerAPIController.GetPrices());
        }

        [Benchmark]
        public async Task<IActionResult> GrpcCommunication()
        {
            var price = new GrpcService.Protos.PriceModel
            {
                CenterCode = _randomizerText.Generate(),
                ClientIssuer = _randomizerText.Generate(),
                CodePaymentTerm = _randomizerText.Generate(),
                CustomerCode = _randomizerText.Generate(),
                DistributionChannel = _randomizerText.Generate(),
                Incoterm = _randomizerText.Generate(),
                IssuerName = _randomizerText.Generate(),
                OrderId = _randomizerText.Generate(),
                OrderType = _randomizerText.Generate(),
                OrganizationCode = _randomizerText.Generate(),
                PaymentForm = _randomizerText.Generate(),
                PostalCode = _randomizerText.Generate(),
            };

            /*
            price.Products.Add(new List<GrpcService.Protos.ProductModel>
                {
                    new GrpcService.Protos.ProductModel
                    {
                        Id = _randomizerText.Generate(),
                        Name = _randomizerText.Generate(),
                        Brand = _randomizerText.Generate(),
                        Model = _randomizerText.Generate()
                    }
                });
            */

            for (var i = 1; i <= 10; i++)
            {
                var product = new GrpcService.Protos.ProductModel
                {
                    Id = _randomizerText.Generate(),
                    Name = _randomizerText.Generate(),
                    Brand = _randomizerText.Generate(),
                    Model = _randomizerText.Generate()
                };
                price.Products.Add(product);
            }

            await _gRpcConsumerAPIController.AddPrice(price);
            return Ok(await _gRpcConsumerAPIController.GetPrices());
        }
    }
}
