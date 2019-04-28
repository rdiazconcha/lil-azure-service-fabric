using System.Threading.Tasks;

namespace Interfaces
{
    public interface IProductsService
    {
        Task<Product> GetProductAsync(string id);
    }

    public class Product
    {
        public string Id { get; set; }
        public string Nombre { get; set; }
    }
}
