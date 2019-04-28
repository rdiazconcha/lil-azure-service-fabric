using System.ServiceModel;
using System.Threading.Tasks;

namespace Interfaces
{
    [ServiceContract]
    public interface IProductsService
    {
        [OperationContract]
        Task<Product> GetProductAsync(string id);
    }

    public class Product
    {
        public string Id { get; set; }
        public string Nombre { get; set; }
    }
}
