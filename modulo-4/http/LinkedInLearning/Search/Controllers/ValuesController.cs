using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.ServiceFabric.Services.Client;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Search.Models;

namespace Search.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(string id)
        {
            var productsService = "fabric:/LinkedInLearning/Products";

            var service = await ResolveAsync(productsService);

            try
            {
                var client = new HttpClient();

                var result = await client.GetAsync(service + $"/api/values/{id}");

                if (result.IsSuccessStatusCode)
                {
                    var content = await result.Content.ReadAsStringAsync();

                    var product = JsonConvert.DeserializeObject<Product>(content);

                    if (product != null)
                    {
                        return Ok(product);
                    }

                    return NotFound();

                }

                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
            
        }

        private async Task<string> ResolveAsync(string name)
        {
            var uri = new Uri(name);

            var resolver = ServicePartitionResolver.GetDefault();

            var service = await resolver.ResolveAsync(uri, ServicePartitionKey.Singleton, CancellationToken.None);

            var addresses = JObject.Parse(service.GetEndpoint().Address);

            var primary = (string)addresses["Endpoints"].First;

            return primary;
        }
    }
}
