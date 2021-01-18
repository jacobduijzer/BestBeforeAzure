using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BestBeforeAzure.Application.Products;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BestBeforeAzure.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<StockController> _logger;

        public StockController(
            IMediator mediator,
            ILogger<StockController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpPost("AddStock")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddStock([FromBody] StockUpdate stockUpdate)
        {
            using (_logger.BeginScope(new Dictionary<string, object>
            {
                {"ProductId", stockUpdate.ProductId},
                {"BestBefore", stockUpdate.BestBefore},
                {"Amount", stockUpdate.Amount}
            }))
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
                    _logger.LogError(ex, ex.Message);
                    return BadRequest();
                }
            }
        }


        [HttpPost("UpdateStock")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateStock([FromBody] StockUpdate stockUpdate)
        {
            using (_logger.BeginScope(new Dictionary<string, object>
            {
                {"ProductId", stockUpdate.ProductId},
                {"BestBefore", stockUpdate.BestBefore},
                {"Amount", stockUpdate.Amount}
            }))
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
                    _logger.LogError(ex, ex.Message);
                    return BadRequest();
                }
            }
        }
    }
}
