using RestSharp;

namespace AWSLambdaConsumoERP.Domain.Entities
{
    public class ObjectSQS
    {
        public string url { get; set; }
        public string dataAPI { get; set; }
        public string method { get; set; }
        public Method methodURL { get; set; }
    }
}
