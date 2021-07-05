using System;
using System.Collections.Generic;
using System.Text;

namespace VendingMachine.Products
{
    public class ProductStock
    {
        public int amount;
        public int Amount
        {
            get { return amount; }
            set
            {
                if (value < 0)
                    throw new Exception();

                amount = value;
            }
        }

        public Product product;

        public ProductStock(int amount, Product product)
        {
            this.amount = amount;
            this.product = product;
        }

        public void ChangeAmount(int amount)
        {
            this.amount += amount;
        }

        public Product GetProduct()
        {
            if (amount > 0)
            {
                ChangeAmount(-1);
                return product;
            }

            throw new OutOfStockException("Product is out of stock.");
        }
    }

    public class OutOfStockException : Exception
    {
        public OutOfStockException(string msg) : base(msg)
        {

        }
    }
}
