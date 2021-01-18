using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using BestBeforeAzure.Application.Products;
using BestBeforeAzure.Domain.Products;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BestBeforeAzure.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("AllProducts")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _mediator
                .Send(new AllProductsQuery.Query())
                .ConfigureAwait(false);

            return Ok(products);
        }

        [HttpPost("CreateProduct")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateProduct([FromBody] string name)
        {
            try
            {
                await _mediator.Publish(new AddProductCommand.Command(name));
                return Accepted();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
    }
}
