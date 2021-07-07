using System;
using System.Collections.Generic;
using System.Text;

namespace VendingMachine.Products.Things
{
    public class Headphones : Product
    {
        private static readonly Random random = new Random();
        public Headphones(string type, int price, string description) : base(type, price, description)
        {

        }

        /// <summary>
        /// Returns a string describing how headphones are used. Randomly sees if headphones are used correctly.
        /// </summary>
        /// <returns>A string describing how headphones are used</returns>
        public override string Use()
        {
            if (IsUnusable) return "These headphones belong in the trash.";

            bool usedWrong = CheckIfUsedWrong();

            if (usedWrong)
            {
                IsUnusable = true;
                return $"You use the {Description} {Type} wrong and have to now live with the consequences.";
            }

            return $"You use the {Description} {Type} and listen to some nice music!";
        }

        /// <summary>
        /// Randomly generates number between 1 and 100.
        /// Returns true if randomly generated number is bigger than 85.
        /// </summary>
        /// <returns>bool if number is bigger than 85</returns>
        public virtual bool CheckIfUsedWrong()
        {
            int randomNumber = random.Next(1, 101);

            return randomNumber > 85;
        }
    }
}
