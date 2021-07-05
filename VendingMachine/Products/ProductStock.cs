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
                    throw new ArgumentException("Amount can't be less than zero.");

                amount = value;
            }
        }

        public Product Product { get; set; }

        public ProductStock(int amount, Product product)
        {
            this.Amount = amount;
            this.Product = product;
        }

        public void ChangeAmount(int amount)
        {
            if (this.amount + amount < 0)
                this.amount = 0;
            else
                this.amount += amount;
        }

        public Product GetProduct()
        {
            if (amount > 0)
            {
                ChangeAmount(-1);
                return Product;
            }

            throw new OutOfStockException("Product is out of stock.");
        }
    }
}
