using System;
using System.Collections.Generic;
using System.Text;
using VendingMachine.Exceptions;
using VendingMachine.Products;

namespace VendingMachine
{
    public class VendingMachine
    {
        private static readonly int[] validAmounts = new int[] { 1, 5, 10, 20, 50, 100, 500, 1000 };

        private readonly ProductStock[] products;

        private int cost;
        public int Cost { get { return cost; } set { cost = value; } }

        private int moneyPool;

        public int MoneyPool
        {
            get { return moneyPool; }
            set
            {
                if (CheckValidAmount(value))
                    moneyPool = value;
            }
        }

        public VendingMachine(int money, ProductStock[] products)
        {
            this.MoneyPool = money;
            this.products = products;
        }

        public Product BuyProduct(int index)
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

        public void AdjustCost(int price)
        {
            if (MoneyPool - price < 0)
                throw new NotEnoughMoneyException("Not enough money to buy product.");

            Cost += price;
            
        }

        public static bool CheckValidAmount(int money)
        {
            if (Array.FindIndex(validAmounts, a => a == money) == -1)
            {
                throw new ArgumentException("Not a valid amount.");
            }

            return true;
        }
       


    }
}
