using System;
using System.Collections.Generic;
using System.Text;

namespace VendingMachine.Products
{
    public abstract class Edible : Product
    {
        private int calories;
        public int Calories { get { return calories; } set { calories = value; } }
        
        public Edible(int calories, string name, int price, string description) : this(name, price, description)
        {
            this.Calories = calories;
        }

        private Edible(string name, int price, string description) : base(name, price, description)
        {

        }
    }
}
