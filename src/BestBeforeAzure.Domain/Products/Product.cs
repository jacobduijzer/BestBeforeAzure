using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using BestBeforeAzure.Domain.Products.Exceptions;
using BestBeforeAzure.Domain.SharedKernel;

namespace BestBeforeAzure.Domain.Products
{
    public class Product : IAggregateRoot
    {
        public Guid Id { get; private set; }

        [JsonPropertyName("name")]
        public string Name { get; private set; }

        public Dictionary<DateTime, int> Stock => _stock;

        private Dictionary<DateTime, int> _stock = new Dictionary<DateTime, int>();

        public static Product Create(string name)
        {
            return Create(Guid.NewGuid(), name); ;
        }

        public static Product Create(Guid id, string name)
        {
            Product product = new Product()
            {
                Id = id,
                Name = name
            };

            //DomainEvents.Raise<ProductCreated>(new ProductCreated() { Product = product });

            return product;
        }

        public void AddStock(Stock stock)
        {
            if (_stock.ContainsKey(stock.BestBefore.Date))
                _stock[stock.BestBefore.Date] += stock.Amount;
            else
                _stock.Add(stock.BestBefore.Date, stock.Amount);

            //DomainEvents.Raise<StockAdded>(new StockAdded() { Product = product });
        }

        public void RemoveStock(Stock stock)
        {
            if (!_stock.ContainsKey(stock.BestBefore.Date))
                throw new NoStockWithThisBestBeforeDateException();

            if (_stock[stock.BestBefore.Date] < stock.Amount)
                throw new NotEnoughStockToRemoveException();

            _stock[stock.BestBefore.Date] -= stock.Amount;

            //DomainEvents.Raise<StockRemoved>(new StockRemoved() { Product = product });
        }
    }
}
