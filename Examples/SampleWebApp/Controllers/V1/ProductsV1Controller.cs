using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SampleWebApp.Contract.V1;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;

namespace SampleWebApp.Controllers.V1
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Produces("application/json")]
    public class ProductsController
    {
        [HttpGet]
        [SwaggerResponse(StatusCodes.Status200OK, "ok", typeof(IEnumerable<Product>))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "internal error", typeof(ErrorResponse))]
        public ActionResult<IEnumerable<Product>> GetProducts()
        {
            return new[]
            {
                new Product { Id = 1, Name = "A product" },
                new Product { Id = 2, Name = "Another product" },
            };
        }

        [HttpGet]
        [Route("paged")]
        [SwaggerResponse(StatusCodes.Status200OK, "ok", typeof(IEnumerable<Product>))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "internal error", typeof(ErrorResponse))]
        public ActionResult<IEnumerable<Product>> GetProductsPaged([FromQuery] PagingParameters _)
        {
            return new[]
            {
                new Product { Id = 1, Name = "A product" },
                new Product { Id = 2, Name = "Another product" },
            };
        }


        [HttpPost]
        [SwaggerResponse(StatusCodes.Status200OK, "ok", typeof(Product))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "internal error", typeof(ErrorResponse))]
        public IActionResult CreateProduct([FromBody]ProductCreate _)
        {
            return new OkObjectResult(new Product());
        }
    }
}
