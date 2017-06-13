using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Swashbuckle.Swagger.Annotations;
using TRex.Metadata;
using WebApiForFlowTest.Models;
using WebApiForFlowTest.MyFolder;

namespace WebApiForFlowTest.Controllers
{
    public class ProductController : ApiController
        {
        public static List<Product> products = new List<Product>
        {
            new Product { Id = 1, Name = "Tomato Soup", Category = "Groceries", Price = 1 },
            new Product { Id = 2, Name = "Yo-yo", Category = "Toys", Price = 3.75 },
            new Product { Id = 3, Name = "Hammer", Category = "Hardware", Price = 16.99 }
        };

        [Metadata ("Get all products")]
        [SwaggerOperation ("GetAllProducts")]
        [SwaggerResponse (HttpStatusCode.OK)]
        [HttpGet]
        [Route("api/products")]
        public IEnumerable<Product> GetAllProducts ()
            {
            return products;
            }

        [Metadata("Get product by id")]
        [SwaggerOperation("GetProduct")]
        [SwaggerResponse(HttpStatusCode.OK)]
        [SwaggerResponse(HttpStatusCode.BadRequest)]
        [HttpGet]
        [Route("api/products/{id}")]
        public IHttpActionResult GetProduct (int id)
            {
            var product = products.FirstOrDefault((p) => p.Id == id);
            if (product == null)
                return BadRequest();

            return Ok(product);
            }

        [Metadata("Register new product")]
        [SwaggerOperation("RegisterNewProduct")]
        [SwaggerResponse(HttpStatusCode.OK)]
        [SwaggerResponse(HttpStatusCode.BadRequest)]
        [HttpPost]
        [Route("api/products")]
        public IHttpActionResult Post ([FromBody] Product product)
            {
            if ( product.Equals(null) )
                return BadRequest();

            products.Add(product);
            WebhookManager.TriggerEvent("new product", product);
            return Ok(product);
            }

        }
    }
