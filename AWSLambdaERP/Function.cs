using Amazon.Lambda.Core;
using Amazon.Lambda.SQSEvents;
using AWSLambda1.Application;
using AWSLambdaConsumoERP.Domain.Entities;
using AWSLambdaConsumoERP.Persistence.Contracts.Persistence;
using Microsoft.Extensions.DependencyInjection;
using NetCoreLambda.DI;
using Newtonsoft.Json;


// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace AWSLambda1;

public class Function
{
    /// <summary>
    /// Default constructor. This constructor is used by Lambda to construct the instance. When invoked in a Lambda environment
    /// the AWS credentials will come from the IAM role associated with the function and the AWS region will be set to the
    /// region the Lambda function is executed in.
    /// </summary>
    private IAsyncRepository<TrazaConsumoERP> _trazaConsumoERP { get; }

    public Function()
    {

        // Get dependency resolver
        var resolver = new DependencyResolver(ConfigureServices);
        _trazaConsumoERP = resolver.ServiceProvider.GetService<IAsyncRepository<TrazaConsumoERP>>();
    }


    /// <summary>
    /// This method is called for every Lambda invocation. This method takes in an SQS event object and can be used 
    /// to respond to SQS messages.
    /// </summary>
    /// <param name="evnt"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    public async Task FunctionHandler(SQSEvent evnt, ILambdaContext context)
    {
        Console.WriteLine($"Beginning to process {evnt.Records.Count} records...");
        foreach (var message in evnt.Records)
        {
            await ProcessMessageAsync(message, context);
        }
    }

    private async Task ProcessMessageAsync(SQSEvent.SQSMessage message, ILambdaContext context)
    {
        context.Logger.LogInformation($"Processed message {message.Body}");
        var input = string.Empty;
        string body = string.Empty;
        string metodo = string.Empty;
        string url = string.Empty;

        try
        {
            var objectSQS = JsonConvert.DeserializeObject<ObjectSQS>(message.Body);
            body = message.Body;
            metodo = objectSQS.method;
            url = objectSQS.url;
            //AWSLambdaConsumoERP.Application.Service service = new AWSLambdaConsumoERP.Application.Service(_trazaConsumoERP);
            //await service.ConsumingERP(objectSQS);
            ServiceERP serviceERP = new ServiceERP(_trazaConsumoERP);
            Console.WriteLine($"Consumir api. {objectSQS.dataAPI}");
            var response = await serviceERP.ConsumirServicioERP(objectSQS.url, objectSQS.dataAPI, objectSQS.methodURL, objectSQS.method);
            //TrazaConsumoERP trazaConsumoERP = new TrazaConsumoERP();
            //trazaConsumoERP.RequestBody = body;
            //trazaConsumoERP.Url = objectSQS.url;
            //trazaConsumoERP.RequestResponse = string.IsNullOrEmpty(response.Content) ? "No se recibe respuesta": response.Content;
            //trazaConsumoERP.FechaRequest = DateTime.Now;
            //trazaConsumoERP.NombreApi = metodo;
            //await _trazaConsumoERP.AddAsync(trazaConsumoERP);
            Console.WriteLine("Processing complete.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error. {ex.Message}");
            input = ex.Message;
            TrazaConsumoERP trazaConsumoERP = new TrazaConsumoERP();
            trazaConsumoERP.RequestBody = body;
            trazaConsumoERP.RequestResponse = ex.Message;
            trazaConsumoERP.FechaRequest = DateTime.Now;
            trazaConsumoERP.NombreApi = metodo;
            trazaConsumoERP.Url = url;
            await _trazaConsumoERP.AddAsync(trazaConsumoERP);
        }
        // TODO: Do interesting work based on the new message
        await Task.CompletedTask;
    }

    private void ConfigureServices(IServiceCollection services)
    {

    }
}