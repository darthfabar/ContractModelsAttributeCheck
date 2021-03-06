using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SampleWebApp.Contract.V2;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;

namespace SampleWebApp.Controllers.V2
{
    [ApiVersion("2.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Produces("application/json")]
    public class ProductsController
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

        [HttpPost]
        [Route("dosomething")]
        [SwaggerResponse(StatusCodes.Status204NoContent, "ok")]
        public IActionResult DoSomething([FromBody] ProductCreateV2 _)
        {
            return new NoContentResult();
        }

        [HttpGet]
        [Route("dummy")]
        [SwaggerResponse(StatusCodes.Status200OK, "ok", typeof(DummyDict))]

        public IActionResult GetDummyModel()
        {
            return new NoContentResult();
        }
    }
}
