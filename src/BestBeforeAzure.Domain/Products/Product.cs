using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using BestBeforeAzure.Domain.Products.Exceptions;
using BestBeforeAzure.Domain.SharedKernel;

namespace BestBeforeAzure.Domain.Products
{
    public class Product : IAggregateRoot
    {
        public Guid Id { get; private set; }

        [JsonPropertyName("name")] public string Name { get; private set; }

        public IReadOnlyCollection<Stock> Stock => _stock.AsReadOnly();

        private List<Stock> _stock = new List<Stock>();

        public static Product Create(string name)
        {
            return Create(Guid.NewGuid(), name);
            ;
        }

        public static Product Create(Guid id, string name)
        {
            Product product = new Product() {Id = id, Name = name};

            //DomainEvents.Raise<ProductCreated>(new ProductCreated() { Product = product });

            return product;
        }

        public void AddStock(Stock stock)
        {
            var stockItem = _stock.SingleOrDefault(item => item.BestBefore.Date.Equals(stock.BestBefore.Date));
            if (stockItem != null)
            {
                var newStockItem = stockItem with {Amount = stockItem.Amount + stock.Amount};
                _stock.Remove(stockItem);
                _stock.Add(newStockItem);
            }
            else
                _stock.Add(stock);

            //DomainEvents.Raise<StockAdded>(new StockAdded() { Product = product });
        }

        public void RemoveStock(Stock stock)
        {
            var stockItem = _stock.SingleOrDefault(item => item.BestBefore.Date.Equals(stock.BestBefore.Date));
            if (stockItem == null)
                throw new NoStockWithThisBestBeforeDateException();

            if (stockItem.Amount < stock.Amount)
                throw new NotEnoughStockToRemoveException();

            var newStockItem = stockItem with {Amount = stockItem.Amount - stock.Amount};
            _stock.Remove(stockItem);
            _stock.Add(newStockItem);

            //DomainEvents.Raise<StockRemoved>(new StockRemoved() { Product = product });
        }
    }
}
