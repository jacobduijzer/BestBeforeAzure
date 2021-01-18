using System;
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
    public class StockController : ControllerBase
    {
        private readonly IMediator _mediator;

        public StockController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateToDoItem([FromBody] string name)
        {
            try
            {
                await _mediator
                    .Publish(new AddStockToProductCommand.Command(new Guid("0881330b-0269-42c8-ba90-8df930972b81"), new Stock(10, DateTime.Now.AddYears(1))))
                    .ConfigureAwait(false);
                return Accepted();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }   
        }
    }
}
