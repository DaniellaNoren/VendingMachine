using System;
using System.Collections.Generic;
using System.Text;

namespace VendingMachine.Products
{
    public class Drink : Edible
    {
        public Drink(int calories, string name, int price, string description) : base(calories, name, price, description)
        {

        } 
        
        public override string Use()
        {
            throw new NotImplementedException();
        }
    }
}
