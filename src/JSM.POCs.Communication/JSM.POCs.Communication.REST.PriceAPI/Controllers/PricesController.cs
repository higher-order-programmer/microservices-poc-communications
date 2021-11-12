using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace JSM.POCs.Communication.REST.PriceAPI.Controllers
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
        }

        // POST api/<PricesController>
        [HttpPost]
        public IActionResult Post(PriceDto priceRequest)
        {
            if (_entries.Count <= 200)
                _entries.Add(priceRequest);

            return Ok();
        }

    }
}
