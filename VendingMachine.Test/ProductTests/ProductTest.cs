using System;
using System.Collections.Generic;
using System.Text;
using VendingMachine.Products.Things;
using Xunit;

namespace VendingMachine.Test.ProductTests
{
    public class ProductTest
    {
        [Fact]
        public void Examine_ReturnCorrectString()
        {
            Product product = new Headphones("Apple", 150, "Cool");

            string examineString = product.Examine();

            Assert.Contains(product.Type, examineString);
            Assert.Contains(product.Price.ToString(), examineString);
            Assert.Contains(product.Description, examineString);

        }

        [Fact]
        public void Product_IsUnusableIsFalse()
        {
            Product product = new Headphones("Apple", 150, "Cool");
            Assert.False(product.IsUnusable);
        }
    }
}
