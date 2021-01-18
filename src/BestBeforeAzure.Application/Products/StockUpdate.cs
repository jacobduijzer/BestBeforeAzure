using System;

namespace BestBeforeAzure.Application.Products
{
    public record StockUpdate(Guid ProductId, DateTime BestBefore, int Amount);
}
