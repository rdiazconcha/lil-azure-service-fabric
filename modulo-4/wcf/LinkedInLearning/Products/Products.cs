using System;
using System.Collections.Generic;
using System.Fabric;
using System.Threading.Tasks;
using Interfaces;
using Microsoft.ServiceFabric.Services.Communication.Runtime;
using Microsoft.ServiceFabric.Services.Communication.Wcf;
using Microsoft.ServiceFabric.Services.Communication.Wcf.Runtime;
using Microsoft.ServiceFabric.Services.Runtime;

namespace Products
{
    /// <summary>
    /// El runtime de Service Fabric crea una instancia de esta clase para cada instancia del servicio.
    /// </summary>
    internal sealed class Products : StatelessService, IProductsService
    {
        public Products(StatelessServiceContext context)
            : base(context)
        { }


        public Task<Product> GetProductAsync(string id)
        {
            var product = new Product() { Id = id, Nombre = Guid.NewGuid().ToString() };
            return Task.FromResult(product);
        }

        protected override IEnumerable<ServiceInstanceListener> CreateServiceInstanceListeners()
        {
            return new ServiceInstanceListener[]
            {
                new ServiceInstanceListener((context) =>
                    new WcfCommunicationListener<IProductsService>(context, this, WcfUtility.CreateTcpListenerBinding(), "ServiceEndpoint"))
            };
        }
    }
}
