using System;
using System.Linq;
using System.Threading.Tasks;
using Interfaces;
using Microsoft.ServiceFabric.Services.Client;
using Microsoft.ServiceFabric.Services.Communication.Client;
using Microsoft.ServiceFabric.Services.Communication.Wcf;
using Microsoft.ServiceFabric.Services.Communication.Wcf.Client;

namespace ProductsClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            if (args == null || !args.Any())
            {
                Console.WriteLine("Debe especificar el identificador del producto");
                return;
            }

            var id = args[0];
            var serviceUri = new Uri("fabric:/LinkedInLearning/Products");

            var binding = WcfUtility.CreateTcpClientBinding();

            var servicePartitionResolver = ServicePartitionResolver.GetDefault();

            var wcfCommunicationClientFactory = new WcfCommunicationClientFactory<IProductsService>(binding, null, servicePartitionResolver);

            var servicePartitionClient = new ServicePartitionClient<WcfCommunicationClient<IProductsService>>(wcfCommunicationClientFactory, serviceUri);

            Console.WriteLine($"Consultando {id} ...");

            var result = await servicePartitionClient.InvokeWithRetryAsync(client => client.Channel.GetProductAsync(id));

            Console.WriteLine($"{result.Id} {result.Nombre}");
        }
    }
}