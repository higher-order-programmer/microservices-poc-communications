using System;
using System.Collections.Generic;

namespace JSM.POCs.Communication.REST.PriceAPI
{
    public class PriceDto
    {
        public string OrderId { get; set; }
        public string ClientIssuer { get; set; }
        public string CustomerCode { get; set; }
        public string IssuerName { get; set; }
        public string CenterCode { get; set; }
        public string Incoterm { get; set; }
        public string Sector { get; set; }
        public string PaymentForm { get; set; }
        public string OrganizationCode { get; set; }
        public string DistributionChannel { get; set; }
        public List<ProductDto> Products { get; set; }
        public string CodePaymentTerm { get; set; }
        public string OrderType { get; set; }
        public DateTime? ScheduleDate { get; set; } = DateTime.Now;
        public string PostalCode { get; set; }
    }
}
