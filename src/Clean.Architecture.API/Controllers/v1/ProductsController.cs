using Clean.Architecture.Business.Products.Queries.GetProducts;
using Microsoft.AspNetCore.Mvc;

namespace Clean.Architecture.API.Controllers.v1
{
    [ApiVersion("1.0")]
    public class ProductsController : ApiControllerBase
    {
        [MapToApiVersion("1.0")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetAllProducts()
        {
            return Ok(await Mediator.Send(new GetProductsQuery()));
        }
    }
}
