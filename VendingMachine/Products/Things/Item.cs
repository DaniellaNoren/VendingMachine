using System;
using System.Collections.Generic;
using System.Text;

namespace VendingMachine.Products.Things
{
    public class Item : Product
    {
        public Item(string name, int price, string description) : base(name, price, description)
        {

        }

        public override string Use()
        {
            return $"You use the {Description} {Name} and feel bad about it.";
        }
    }
}
