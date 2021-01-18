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

        [HttpPost("AddStock")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddStock([FromBody] StockUpdate stockUpdate)
        {
            try
            {
                await _mediator
                    .Publish(new AddStockToProductCommand.Command(stockUpdate))
                    .ConfigureAwait(false);
                return Accepted();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }


        [HttpPost("UpdateStock")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateStock([FromBody] StockUpdate stockUpdate)
        {
            try
            {
                await _mediator
                    .Publish(new UpdateStockCommand.Command(stockUpdate))
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
