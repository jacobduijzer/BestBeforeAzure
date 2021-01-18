using System;
namespace BestBeforeAzure.Domain.Products
{
    public record Stock(int Amount, DateTime BestBefore);
}
