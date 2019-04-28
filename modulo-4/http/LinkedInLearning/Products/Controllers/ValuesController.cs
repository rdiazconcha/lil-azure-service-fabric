using System;
using Microsoft.AspNetCore.Mvc;
using Products.Models;

namespace Products.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [HttpGet("{id}")]
        public ActionResult Get(string id)
        {
            var product = new Product() { Id = id, Nombre = Guid.NewGuid().ToString() };
            return Ok(product);
        }

    }
}
