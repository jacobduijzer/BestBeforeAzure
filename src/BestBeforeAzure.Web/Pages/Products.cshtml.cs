using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using BestBeforeAzure.Application.Products;
using BestBeforeAzure.Domain.Products;
using MediatR;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BestBeforeAzure.Web.Pages
{
    public class Products : PageModel
    {
        private readonly IMediator _mediator;

        public IEnumerable<Product> ProductsList { get; set; }
        
        public Products(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task OnGetAsync()
        {
            ProductsList = await _mediator
                .Send(new AllProductsQuery.Query())
                .ConfigureAwait(false);
        }
    }
}
