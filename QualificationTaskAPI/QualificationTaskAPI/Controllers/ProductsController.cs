using MediatR;
using Microsoft.AspNetCore.Mvc;
using QualificationTask.Application.Features.Products.Commands.AddProduct;
using QualificationTask.Application.Features.Products.Queries.GetProducts;

namespace QualificationTaskAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator mediator;

        public ProductsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetProductsResponse>>> GetProducts(CancellationToken cancellationToken)
        {
            return Ok(await mediator.Send(new GetProductsQuery(), cancellationToken));

        }

        [HttpPost]
        public async Task<ActionResult<int>> AddProduct([FromBody] AddProductRequest request, CancellationToken cancellationToken)
        {
            int newProductId = await mediator.Send(new AddProductCommand(request), cancellationToken);

            return Created(string.Empty, newProductId);
        }
    }
}
