using System.Threading;
using System.Threading.Tasks;
using BestBeforeAzure.Domain.Products;
using BestBeforeAzure.Domain.SharedKernel;
using MediatR;

namespace BestBeforeAzure.Application.Products
{
    public class AddProductCommand
    {
        public record Command(string Name) : INotification;

        public class Handler : INotificationHandler<Command>
        {
            private readonly IRepository<Product> _productRepository;

            public Handler(IRepository<Product> productRepository)
            {
                _productRepository = productRepository;
            }

            public async Task Handle(Command notification, CancellationToken cancellationToken)
            {
                var product = Product.Create(notification.Name);
                await _productRepository.Add(product);
            }
        }
    }
}
