using System.Threading;
using System.Threading.Tasks;
using BestBeforeAzure.Domain.Products;
using BestBeforeAzure.Domain.Products.Exceptions;
using BestBeforeAzure.Domain.SharedKernel;
using MediatR;

namespace BestBeforeAzure.Application.Products
{
    public class UpdateStockCommand
    {
        public record Command(StockUpdate StockUpdate) : INotification;

        public class Handler : INotificationHandler<Command>
        {
            private readonly IRepository<Product> _productRepository;

            public Handler(IRepository<Product> productRepository)
            {
                _productRepository = productRepository;
            }

            public async Task Handle(Command notification, CancellationToken cancellationToken)
            {
                var product = await _productRepository
                    .FindById(notification.StockUpdate.ProductId)
                    .ConfigureAwait(false);

                if (product == null)
                    throw new ProductNotFoundException(
                        $"Product with ID {notification.StockUpdate.ProductId} not found");

                product.UpdateStock(new Stock(notification.StockUpdate.Amount, notification.StockUpdate.BestBefore));

                await _productRepository
                    .Update(product)
                    .ConfigureAwait(false);
            }
        }
    }
}
