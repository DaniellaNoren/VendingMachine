using System;
using System.Collections.Generic;
using System.Text;

namespace VendingMachine
{
    abstract public class Product
    {
        int price;
        public int Price { get { return price; } set { price = value; } }

        string name;
        public string Type { get { return name; } set { name = value; } }

        string description;
        public string Description { get { return description; } set { description = value; } }

        private bool isUnusable;

        public bool IsUnusable { get { return isUnusable; } set { isUnusable = value; } }
        public Product(string type, int price, string description)
        {
            this.Price = price;
            this.Type = type;
            this.Description = description;
        }

        public virtual string Examine()
        {
            return $"Type: {Type}, Cost: {Price}, Description: {Description}";
        }

        public abstract string Use();

    }
}
