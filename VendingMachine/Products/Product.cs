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
        public string Name { get { return name; } set { name = value; } }

        string description;
        public string Description { get { return description; } set { description = value; } }
        
        public Product(string name, int price, string description)
        {
            this.Price = price;
            this.Name = name;
            this.Description = description;
        }

        public virtual string Examine()
        {
            return $"Type: {Name}, Cost: {Price}, Description: {Description}";
        }

        public abstract string Use();
    }
}
