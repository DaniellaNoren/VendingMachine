using System;
using System.Collections.Generic;
using System.Text;

namespace VendingMachine.Products.Food
{
    public class Sandwich : Consumable
    {
        private bool hasMayo;
        public bool HasMayo { get { return hasMayo; } set { hasMayo = value; } }

        public Sandwich(int calories, string type, int price, string description, bool hasMayo = false) : base(calories, type, price, description)
        {
            this.HasMayo = hasMayo;
        }

        /// <summary>
        /// Returns what the sandwich smells like.
        /// </summary>
        /// <returns>String of smell-description</returns>
        public string Smell()
        {
            if (HasMayo)
                return "Smells like mayonnaise";

            return "Smells like a sandwich.";
        }

        /// <summary>
        /// Returns string describing how Sandwich is used.
        /// </summary>
        /// <returns>A string describing how sandwich is used</returns>
        public override string Use() 
        {
            if (IsUnusable) return "This sandwich is already eaten!";

            this.IsUnusable = true;

            if (hasMayo)
            {
                return $"You eat the mayonnaise-covered {Description} {Type} for {Calories} kcal and feel bad.";
            }

            return $"You eat the {Description} {Type} for {Calories} kcal and feel amazing.";
        }
    }
}
