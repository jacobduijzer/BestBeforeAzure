using System;
using System.Threading;
using System.Threading.Tasks;
using BestBeforeAzure.Domain.Products;
using BestBeforeAzure.Domain.SharedKernel;
using MediatR;

namespace BestBeforeAzure.Application.Products
{
    public class AddProductCommand
    {
        public record Command(string Name) : IRequest<Guid>;

        public class Handler : IRequestHandler<Command, Guid>
        {
            private readonly IRepository<Product> _productRepository;

            public Handler(IRepository<Product> productRepository)
            {
                _productRepository = productRepository;
            }

            public async Task<Guid> Handle(Command notification, CancellationToken cancellationToken)
            {
                var product = Product.Create(notification.Name);
                await _productRepository.Add(product);
                return product.Id;
            }
        }
    }
}
