using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.ServiceFabric.Services.Remoting.Client;

namespace Search.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(string id)
        {
            var productsService = ServiceProxy.Create<IProductsService>(new Uri("fabric:/LinkedInLearning/Products"));
            var product = await productsService.GetProductAsync(id);
            return Ok(product);
        }
    }
}
