using System;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Services.Remoting;

namespace Interfaces
{
    public interface IProductsService : IService
    {
        Task<Product> GetProductAsync(string id);
    }

    public class Product
    {
        public string Id { get; set; }
        public string Nombre { get; set; }
    }
}
