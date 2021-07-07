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
        public void VendingMachine_ShouldThrowExceptionIfNegativeMoneyPool()
        {
            Assert.Throws<ArgumentException>(() => new VendingMachine(-1, new ProductStock[0]));
        }

        [Fact]
        public void AdjustCost_ShouldThrowException()
        {
            VendingMachine vm = new VendingMachine(20, new ProductStock[0]);
            Assert.Throws<NotEnoughMoneyException>(() => vm.AdjustCostAndMoneyPool(21));
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

        private static ProductStock[] products = GetProducts();

        public static ProductStock[] GetProducts()
        {
            return new ProductStock[] {
                new ProductStock(1,new SodaCan(42, "Coke", 12, "Fizzy")),
                new ProductStock(1, new Sandwich(100, "Chocolate bar", 12, "Tasty")),
                new ProductStock(1, new Headphones("Headphones", 150, "Cool")) };
        }

        [Fact]
        public void Purchase_ShouldReturnCorrectProduct()
        {
            VendingMachine vm = new VendingMachine(500, GetProducts());

            Product pr = vm.Purchase(1);
            Assert.Equal(products[1].Product.Examine(), pr.Examine());
        }  
        
        [Fact]
        public void Purchase_ShouldAddToCost()
        {
            VendingMachine vm = new VendingMachine(500, GetProducts());

            vm.Purchase(1);
            Assert.Equal(products[1].Product.Price, vm.TotalCost);
        }

        [Theory]
        [InlineData(5)]
        [InlineData(-1)]
        public void Purchase_ShouldThrowIndexOutOfRangeException(int index)
        {
            VendingMachine vm = new VendingMachine(500, products);
            Assert.Throws<IndexOutOfRangeException>(() => vm.Purchase(index));
        }   

        [Fact]
        public void Purchase_ShouldThrowOutOfStockException()
        {
            VendingMachine vm = new VendingMachine(500, new ProductStock[] { new ProductStock(0, new SodaCan(5, "Coke", 50, "Description")) });
            
            Assert.Throws<OutOfStockException>(() => vm.Purchase(0));
        }

        [Fact]
        public void EndTransaction_ShouldReturnCorrectChange()
        {
            VendingMachine vm = new VendingMachine(500, new ProductStock[] { new ProductStock(2, new SodaCan(75, "Coke", 5, "Description")) });
            vm.Purchase(0);
            Dictionary<int, int> change = vm.EndTransaction();

            change.TryGetValue(100, out int value100);
            change.TryGetValue(50, out int value50);
            change.TryGetValue(20, out int value20);
            change.TryGetValue(5, out int value5);
            
            Assert.Equal(4, value100);
            Assert.Equal(1, value50);
            Assert.Equal(2, value20);
            Assert.Equal(1, value5);
            Assert.True(change.Count == 4);

            Assert.False(change.ContainsKey(1));
            Assert.False(change.ContainsKey(1000));
            Assert.False(change.ContainsKey(500));

        }

        [Theory]
        [InlineData(5, 505)]
        [InlineData(100, 600)]
        [InlineData(1, 501)]
        [InlineData(500, 1000)]
        public void InsertMoney_AddToMoneyPool(int amount, int expected)
        {
            VendingMachine vm = new VendingMachine(500, new ProductStock[0]);
            vm.InsertMoney(amount);

            Assert.Equal(vm.MoneyPool, expected);
        }

        [Fact]
        public void ShowAll_ShouldReturnProductStock()
        {
            VendingMachine vm = new VendingMachine(500, products);
            ProductStock[] productStock = vm.ShowAll();
            Assert.Equal(products, productStock);
        }


    }
}
