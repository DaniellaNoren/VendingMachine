﻿using System;
using System.Collections.Generic;
using System.Text;

namespace VendingMachine.Products
{
    public abstract class Consumable : Product
    {
        private int calories;
        public int Calories { get { return calories; } set { calories = value; } }
        public Consumable(int calories, string name, int price, string description) : this(name, price, description)
        {
            this.Calories = calories;
        }

        private Consumable(string name, int price, string description) : base(name, price, description)
        {

        }

        public override string Use()
        {
            if (IsUnusable)
                return $"You can't consume this item! It is not safe.";

            return $"You consumed {Description} {Name} for {Calories} kcal. Yummy!";
        }
    }
}
