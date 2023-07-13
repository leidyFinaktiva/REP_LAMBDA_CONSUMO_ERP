using AWSLambdaConsumoERP.Application.Resources;
using AWSLambdaConsumoERP.Domain.Entities;
using AWSLambdaConsumoERP.Persistence.Contracts.Persistence;
using RestSharp;

namespace AWSLambdaConsumoERP.Application
{
    public class Service
    {
        private readonly IAsyncRepository<TrazaConsumoERP> _trazaConsumoERP;

        public Service(IAsyncRepository<TrazaConsumoERP> trazaConsumoERP) 
        { 
            _trazaConsumoERP = trazaConsumoERP;
        }
        public async Task ConsumingERP(ObjectSQS objectSQS)
        {
            var client = new RestClient();
            var request = new RestRequest(objectSQS.url, objectSQS.methodURL);
            request.Method = objectSQS.methodURL;
            if (objectSQS.methodURL != Method.Get)
            {
                var data = objectSQS.dataAPI;
                request.AddJsonBody(data);
            }
            Console.WriteLine($"Se va a llamar el api de clientes:");
            var responseApi = client.Execute(request);
            Console.WriteLine($"Hizo petición: {responseApi.StatusCode} {responseApi.Content}");
            TrazaConsumoERP traza = new TrazaConsumoERP();
            traza.Url = objectSQS.url;
            traza.FechaRequest = DateTime.Now;
            traza.NombreApi = objectSQS.method;
            traza.RequestBody = objectSQS.dataAPI;
            traza.RequestResponse = responseApi.Content;
            await _trazaConsumoERP.AddAsync(traza);
        }


    }
}
