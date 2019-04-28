using System;
using System.Collections.Generic;
using System.Fabric;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Interfaces;
using Microsoft.ServiceFabric.Services.Communication.Runtime;
using Microsoft.ServiceFabric.Services.Runtime;
using Microsoft.ServiceFabric.Services.Remoting.Runtime;

namespace Products
{
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
            var listeners = this.CreateServiceRemotingInstanceListeners();
            return listeners;
        }
    }
}
