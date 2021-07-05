using System;
using System.Collections.Generic;
using System.Text;
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
            if (index >= products.Length)
                throw new Exception();

            if (products[index].amount < 0)
                throw new Exception();

            Product pr = products[index].GetProduct();

            if (MoneyPool - pr.Price < 0)
                throw new Exception();

            Cost += pr.Price;
            MoneyPool -= pr.Price;

            return pr;

        }

        public bool CheckValidAmount(int money)
        {
            if (Array.FindIndex(validAmounts, a => a == money) == -1)
            {
                throw new Exception();
            }

            return true;
        }
        public void AddToMoneyPool(int money)
        {
            if (CheckValidAmount(money))
            {
                MoneyPool += money;
            };
        }

        public void IncreaseCost(int price)
        {
            cost += price;
        }


    }
}
