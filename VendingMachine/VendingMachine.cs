using System;
using System.Collections.Generic;
using System.Text;
using VendingMachine.Exceptions;
using VendingMachine.Products;

namespace VendingMachine
{
    public class VendingMachine : IVendingMachine
    {
        private static readonly int[] validAmounts = new int[] { 1000, 500, 100, 50, 20, 10, 5, 1 };

        private readonly ProductStock[] products;

        private int totalCost;
        public int TotalCost
        {
            get { return totalCost; }
            set { totalCost = value; }
        }

        private int moneyPool;

        public int MoneyPool
        {
            get { return moneyPool; }
            set
            {
                if (value < 0) throw new ArgumentException("MoneyPool can't be negative");
                moneyPool = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="money">Amount of money to buy products with</param>
        /// <param name="products">Products to sell</param>
        public VendingMachine(int money, ProductStock[] products)
        {
            this.MoneyPool = money;
            this.products = products;
        }

        /// <summary>
        /// Adjust cost with the incoming integer. Throws exception if MoneyPool is less than new total cost.
        /// </summary>
        /// <param name="price">Price to adjust total cost with</param>
        /// <exception cref="NotEnoughMoneyException">If MoneyPool is less than new total cost</exception>
        public void AdjustCost(int price)
        {
            if (MoneyPool - price < 0)
                throw new NotEnoughMoneyException("Not enough money to buy product.");

            TotalCost += price;

        }

        /// <summary>
        /// Checks that incoming int is part of validAmounts-array (1000, 500, 100, 50, 20, 10, 5, 1)
        /// </summary>
        /// <param name="money">Integer to check</param>
        /// <returns>True if integer is part of validAmounts-array</returns>
        /// <exception cref="ArgumentException">If integer is not valid amount</exception>
        public static bool CheckValidAmount(int money)
        {
            if (Array.FindIndex(validAmounts, a => a == money) == -1)
            {
                throw new ArgumentException("Not a valid amount.");
            }

            return true;
        }

        /// <summary>
        /// Returns and adjust stock of the chosen product.
        /// Checks if user has enough money and adjusts cost-total.
        /// </summary>
        /// <param name="index">Index of product to be purchased</param>
        /// <returns>Chosen Product</returns>
        /// <exception cref="IndexOutOfRangeException">If index is out of range</exception>
        /// <exception cref="OutOfStockException">If Product is out of stock</exception>
        /// <exception cref="NotEnoughMoneyException">If MoneyPool is smaller than cost</exception>
        public Product Purchase(int index)
        {
            if (index < 0 || index >= products.Length)
                throw new IndexOutOfRangeException("Index out of range.");

            Product pr;

            try
            {
                pr = products[index].GetProduct();
            }
            catch
            {
                throw;
            }

            AdjustCost(pr.Price);

            return pr;

        }

        /// <summary>
        /// Returns all products
        /// </summary>
        /// <returns>Array of ProductStock</returns>
        public ProductStock[] ShowAll()
        {
            return products;
        }

        /// <summary>
        /// Inserts money in the moneypool if it is of valid amount
        /// </summary>
        /// <param name="money">Money to be inserted</param>
        public void InsertMoney(int money)
        {
            if (CheckValidAmount(money))
                MoneyPool += money;
        }

        /// <summary>
        /// Calculates and returns the correct change in a Dictionary consisting of
        /// the amount (Key) and how many of the amount (Value)
        /// </summary>
        /// <returns><Dictionary>Amount of change</Dictionary></returns>
        public Dictionary<int, int> EndTransaction()
        {
            Dictionary<int, int> change = new Dictionary<int, int>();

            int leftOverMoney = MoneyPool - TotalCost;

            foreach (int value in validAmounts)
            {
                if (leftOverMoney % value < leftOverMoney)
                {
                    change.Add(value, leftOverMoney / value);
                    leftOverMoney -= value * (leftOverMoney / value);
                }
            }

            return change;
        }
    }
}
