using System;
using System.Collections.Generic;
using System.Text;

namespace VendingMachine.Products.Food
{
    public class Food : Edible
    {
        public Food(int calories, string name, int price, string description) : base(calories, name, price, description)
        {

        }
        public override string Use()
        {
            throw new NotImplementedException();
        }
    }
}
