using Amazon.Lambda.Core;
using Amazon.Lambda.SQSEvents;
using AWSLambdaConsumoERP.Application;
using AWSLambdaConsumoERP.Domain.Entities;
using AWSLambdaConsumoERP.Persistence.Contracts.Persistence;
using Microsoft.Extensions.DependencyInjection;
using NetCoreLambda.DI;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text.Json.Serialization;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace AWSLambda1;

public class Function
{
    private IAsyncRepository<TrazaConsumoERP> _trazaConsumoERP { get; }

    public Function()
    {

        // Get dependency resolver
        var resolver = new DependencyResolver(ConfigureServices);
        _trazaConsumoERP = resolver.ServiceProvider.GetService<IAsyncRepository<TrazaConsumoERP>>();
    }

    /// <summary>
    /// A simple function that takes a string and does a ToUpper
    /// </summary>
    /// <param name="input"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    public async Task<string> FunctionHandler(SQSEvent _sqsEvent,ILambdaContext context)
    {
        TrazaConsumoERP trazaConsumoERP1 = new TrazaConsumoERP();
        trazaConsumoERP1.RequestBody = "Empezó";
        trazaConsumoERP1.RequestResponse = "proceso exitoso";
        trazaConsumoERP1.FechaRequest = DateTime.Now;
        trazaConsumoERP1.NombreApi = "Clientes empezó";
        await _trazaConsumoERP.AddAsync(trazaConsumoERP1);
        var input = string.Empty;
        Console.WriteLine($"Beginning to process {_sqsEvent.Records.Count} records...");
        string body = string.Empty;
        string metodo = string.Empty;

        foreach (var record in _sqsEvent.Records)
        {
            Console.WriteLine($"Message ID: {record.MessageId}");
            Console.WriteLine($"Event Source: {record.EventSource}");

            Console.WriteLine($"Record Body:");
            Console.WriteLine(record.Body);
            var objectSQS = JsonConvert.DeserializeObject<ObjectSQS>(record.Body);
            body = record.Body;
            metodo = objectSQS.method;
            Service service = new Service(_trazaConsumoERP);
            await service.ConsumingERP(objectSQS);
            TrazaConsumoERP trazaConsumoERP = new TrazaConsumoERP();
            trazaConsumoERP.RequestBody = body;
            trazaConsumoERP.RequestResponse = "proceso exitoso";
            trazaConsumoERP.FechaRequest = DateTime.Now;
            trazaConsumoERP.NombreApi = metodo;
            await _trazaConsumoERP.AddAsync(trazaConsumoERP);
        }

        Console.WriteLine("Processing complete.");

        try
        {
            ObjectSQS objectSQS = new ObjectSQS();
            Service service = new Service(_trazaConsumoERP);
            await service.ConsumingERP(objectSQS);
            return $"Processed {_sqsEvent.Records.Count} records.";
        }
        catch (Exception ex) { 
            input = ex.Message;
            TrazaConsumoERP trazaConsumoERP = new TrazaConsumoERP();
            trazaConsumoERP.RequestBody = body;
            trazaConsumoERP.RequestResponse = ex.Message;
            trazaConsumoERP.FechaRequest = DateTime.Now;
            trazaConsumoERP.NombreApi = metodo;
            await _trazaConsumoERP.AddAsync(trazaConsumoERP);
            return input;
        }
    }
    private void ConfigureServices(IServiceCollection services)
    {

    }
}
