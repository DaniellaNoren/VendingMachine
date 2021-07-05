using System;
using System.Collections.Generic;
using System.Text;

namespace VendingMachine.Products.Things
{
    public class Item : Product
    {
        private static readonly Random random = new Random();

        private bool isBroken;
        public bool IsBroken { get { return isBroken; } set { isBroken = value; } }
        public Item(string name, int price, string description) : base(name, price, description)
        {
            
        }

        public override string Use()
        {
            int randomNumber = random.Next(1, 101);

            if(randomNumber > 85)
            {
                IsBroken = true;
                return $"You use the {Description} {Name} and you broke it!!!";
            }

            return $"You use the {Description} {Name} and feel kinda bad about it.";
        }
    }
}
