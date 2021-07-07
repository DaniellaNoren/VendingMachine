using System;
using System.Collections.Generic;
using System.Text;
using VendingMachine.Products;
using Xunit;

namespace VendingMachine.Test
{
    public class ProductStockTests
    {
        private readonly Product product = new SodaCan(70, "Coke", 12, "Fizzy drink");
       
        [Fact]
        public void ProductStock_ShouldThrowArgumentExceptionIfAmountIsNegative()
        {
            Assert.Throws<ArgumentException>(() => new ProductStock(-1, product));
        }

        [Theory]
        [InlineData(2, -2, 0)]
        [InlineData(5, -1, 4)]
        [InlineData(2, 2, 4)]
        [InlineData(0, 6, 6)]
        [InlineData(5, -6, 0)]
        public void ChangeAmount_ShouldUpdateAmount(int amount, int amountChange, int expected)
        {
            ProductStock ps = new ProductStock(amount, product);
            ps.ChangeStock(amountChange);

            Assert.Equal(expected, ps.Stock);
        }

        [Fact]
        public void GetProduct_ShouldDecreaseAmountWhenGetProduct()
        {
            ProductStock ps = new ProductStock(2, product);
            ps.GetProduct();
            Assert.Equal(1, ps.Stock);
        }

        [Fact]
        public void GetProduct_ShouldReturnProduct()
        {
            ProductStock ps = new ProductStock(2, product);
            Product pr = ps.GetProduct();

            Assert.Equal(product, pr);
        }

        [Fact]
        public void GetProduct_ShouldThrowOutOfStockException()
        {
            ProductStock ps = new ProductStock(1, product);
            ps.GetProduct();
            Assert.Throws<OutOfStockException>(() => ps.GetProduct());
        }


    }
}
