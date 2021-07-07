using System;
using System.Collections.Generic;
using System.Text;

namespace VendingMachine.Products
{
    public class ProductStock
    {
        public int stock;
        public int Stock
        {
            get { return stock; }
            set
            {
                if (value < 0)
                    throw new ArgumentException("Amount can't be less than zero.");

                stock = value;
            }
        }

        public Product Product { get; set; }

        public ProductStock(int amount, Product product)
        {
            this.Stock = amount;
            this.Product = product;
        }

        /// <summary>
        /// Adds the amount to stock. If the amount causes stock to go negative, stock is zero.
        /// </summary>
        /// <param name="amount">Amount to be added/subtracted to stock</param>
        public void ChangeStock(int amount)
        {
            if (this.stock + amount < 0)
                this.stock = 0;
            else
                this.stock += amount;
        }

        /// <summary>
        /// Returns the product if stock is greater than zero. Otherwise exception is thrown.
        /// Stock decreases by one if Product is returned.
        /// </summary>
        /// <returns>Product</returns>
        /// <exception cref="OutOfStockException"></exception>
        public Product GetProduct()
        {
            if (stock > 0)
            {
                ChangeStock(-1);
                return Product;
            }

            throw new OutOfStockException("Product is out of stock.");
        }
    }
}
