using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SampleWebApp.Contract.V2;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;

namespace SampleWebApp.Controllers
{
    [ApiVersion("2.0")]
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    public class ProductsV2Controller
    {
        [HttpGet]
        [SwaggerResponse(StatusCodes.Status200OK, "ok", typeof(IEnumerable<ProductV2>))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "internal error", typeof(ErrorResponseV2))]
        public ActionResult<IEnumerable<ProductV2>> GetProducts()
        {
            return new[]
            {
                new ProductV2 { Id = 1, Name = "A product" },
                new ProductV2 { Id = 2, Name = "Another product" },
            };
        }


        [HttpGet]
        [Route("paged")]
        [SwaggerResponse(StatusCodes.Status200OK, "ok", typeof(IEnumerable<ProductV2>))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "internal error", typeof(ErrorResponseV2))]
        public ActionResult<IEnumerable<ProductV2>> GetProductsPaged([FromQuery] PagingParametersV2 _)
        {
            return new[]
            {
                new ProductV2 { Id = 1, Name = "A product" },
                new ProductV2 { Id = 2, Name = "Another product" },
            };
        }

        [HttpPost]
        [SwaggerResponse(StatusCodes.Status200OK, "ok", typeof(ProductV2))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "internal error", typeof(ErrorResponseV2))]
        public IActionResult CreateProduct([FromBody]ProductCreateV2 _)
        {
            return new OkObjectResult(new ProductV2());
        }
    }
}
