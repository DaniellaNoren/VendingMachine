using System;
using System.Collections.Generic;
using System.Text;
using VendingMachine.Exceptions;
using VendingMachine.Products;
using VendingMachine.Products.Food;
using VendingMachine.Products.Things;
using Xunit;

namespace VendingMachine.Test
{
    public class VendingMachineTests
    {
        [Fact]
        public void VendingMachine_ShouldThrowExceptionWhenInvalidMoneyPool()
        {
            Assert.Throws<ArgumentException>(() => new VendingMachine(-1, new ProductStock[0]));
        }

        [Fact]
        public void AdjustCost_ShouldThrowException()
        {
            VendingMachine vm = new VendingMachine(20, new ProductStock[0]);
            Assert.Throws<NotEnoughMoneyException>(() => vm.AdjustCost(21));
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(105)]
        [InlineData(1001)]
        [InlineData(2)]
        [InlineData(0)]
        public void CheckValidAmount_ShouldThrowExceptionWhenNotValidAmount(int amount)
        {
            Assert.Throws<ArgumentException>(() => VendingMachine.CheckValidAmount(amount));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(10)]
        [InlineData(1000)]
        [InlineData(500)]
        [InlineData(50)]
        public void CheckValidAmount_ShouldReturnTrue(int amount)
        {
            Assert.True(VendingMachine.CheckValidAmount(amount));
        }

        public static ProductStock[] GetProducts()
        {
            return new ProductStock[] {
                new ProductStock(1,new Drink(42, "Coke", 12, "Fizzy")),
                new ProductStock(1, new Food(100, "Chocolate bar", 12, "Tasty")),
                new ProductStock(1, new Item("Headphones", 150, "Cool")) };
        }

        [Fact]
        public void BuyProduct_ShouldReturnCorrectProduct()
        {
            VendingMachine vm = new VendingMachine(500, GetProducts());

            Product pr = vm.Purchase(1);
            Assert.Equal(GetProducts()[1].Product.Examine(), pr.Examine());
        }  
        
        [Fact]
        public void BuyProduct_ShouldAddToCost()
        {
            VendingMachine vm = new VendingMachine(500, GetProducts());

            vm.Purchase(1);
            Assert.Equal(GetProducts()[1].Product.Price, vm.Cost);
        }

        [Theory]
        [InlineData(5)]
        [InlineData(-1)]
        public void BuyProduct_ShouldThrowIndexOutOfRangeException(int index)
        {
            VendingMachine vm = new VendingMachine(500, GetProducts());
            Assert.Throws<IndexOutOfRangeException>(() => vm.Purchase(index));
        }   

        [Fact]
        public void BuyProduct_ShouldThrowOutOfStockException()
        {
            VendingMachine vm = new VendingMachine(500, new ProductStock[] { new ProductStock(0, new Drink(5, "Coke", 50, "Description")) });
            
            Assert.Throws<OutOfStockException>(() => vm.Purchase(0));
        }

   
    }
}
