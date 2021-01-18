using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BestBeforeAzure.Domain.Products;
using BestBeforeAzure.Domain.SharedKernel;
using MediatR;

namespace BestBeforeAzure.Application.Products
{
    public class AllProductsQuery
    {
        public record Query() : IRequest<IEnumerable<Product>>;

        public class Handler : IRequestHandler<Query, IEnumerable<Product>>
        {
            private readonly IRepository<Product> _productRepository;

            public Handler(IRepository<Product> productRepository)
            {
                _productRepository = productRepository;
            }

            public async Task<IEnumerable<Product>> Handle(Query request, CancellationToken cancellationToken) =>
                await _productRepository
                    .Find(product => true)
                    .ConfigureAwait(false);
        }
    }
}
