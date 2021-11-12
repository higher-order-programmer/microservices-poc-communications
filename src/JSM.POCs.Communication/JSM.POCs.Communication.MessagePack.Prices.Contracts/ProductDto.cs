using MessagePack;

namespace JSM.POCs.Communication.MessagePack.Price.Contracts
{
    [MessagePackObject(keyAsPropertyName:true)]
    public class ProductDto
    {
        public int id { get; set; }
        public string name { get; set; }
        public string model { get; set; }
        public string brand { get; set; }
    }
}
