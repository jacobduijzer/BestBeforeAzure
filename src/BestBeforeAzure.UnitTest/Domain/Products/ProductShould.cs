using System;
using System.Linq;
using BestBeforeAzure.Domain.Products;
using BestBeforeAzure.Domain.Products.Exceptions;
using FluentAssertions;
using Xunit;

namespace BestBeforeAzure.UnitTest.Domain.Products
{
    public class ProductShould
    {
        [Fact]
        public void ConstructWithNewGuid()
        {
            // ARRANGE & ACT
            var sut = Product.Create("Test");

            // ASSERT
            sut.Id.Should().NotBeEmpty();
            sut.Name.Should().Be("Test");
        }

        [Fact]
        public void AddStock()
        {
            // ARRANGE
            var sut = new Product();
            var stock = new Stock(1, DateTime.Now);

            // ACT
            sut.AddStock(stock);

            // ASSERT
            sut.Stock.Should().HaveCount(1);
            sut.Stock.First().BestBefore.Date.Should().Be(DateTime.Now.Date);
            sut.Stock.First().Amount.Should().Be(1);
        }

        [Theory]
        [InlineData(3, 4)]
        [InlineData(0, 1)]
        public void ThrowWhenStockAmountNotEnoughToRemove(int amountInStock, int amountToRemove)
        {
            // ARRANGE
            var sut = new Product();
            var availableStock = new Stock(amountInStock, DateTime.Now);
            sut.AddStock(availableStock);

            var stock = new Stock(amountToRemove, DateTime.Now);

            // ACT & ASSERT
            new Action(() => sut.RemoveStock(stock))
                .Should().Throw<NotEnoughStockToRemoveException>();
        }

        [Fact]
        public void ThrowWhenRemovingNotExistingStock()
        {
            // ARRANGE
            var sut = new Product();
            var stock = new Stock(1, DateTime.Now);

            // ACT & ASSERT
            new Action(() => sut.RemoveStock(stock))
                .Should().Throw<NoStockWithThisBestBeforeDateException>();
        }
    }
}
