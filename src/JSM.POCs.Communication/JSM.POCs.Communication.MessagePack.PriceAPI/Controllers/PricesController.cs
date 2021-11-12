using JSM.POCs.Communication.MessagePack.Price.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace JSM.POCs.Communication.MessagePack.PriceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PricesController : ControllerBase
    {
        private static readonly IList<PriceDto> _entries = new List<PriceDto>();

        // GET: api/<PricesController>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_entries);
            //ja retorna serializado
        }

        // POST api/<PricesController>
        [HttpPost]
        public IActionResult Post(PriceDto priceRequest)
        {
            //simula alguma operação com o payload. (Já desserializado)
            if (_entries.Count <= 200)
                _entries.Add(priceRequest);

            return Ok();
        }

    }
}
