using System;
using System.Collections.Generic;
using System.Text;

namespace VendingMachine.Products
{
    public class SodaCan : Consumable
    {
        private bool isCarbonated;
        public bool IsCarbonated { get { return isCarbonated; } set { isCarbonated = value; } }

        private int pant;
        private int Pant
        {
            get { return pant; }
            set
            {
                if (value < 0)
                    throw new ArgumentException();
                pant = value;
            }
        }

        private int maxSips;
        private int MaxSips { 
            get { return maxSips; } 
            set { if (value < 1) 
                    throw new ArgumentException("MaxSips cannot be less than 1"); 
                maxSips = value; } 
        }

        private int sips;

        public int Sips
        {
            get { return sips; }
            set { if (value < 0 || value > maxSips) 
                    throw new ArgumentException("Invalid number of sips!"); 
                sips = value; 
            }
        }
        public SodaCan(int calories, string name, int price, string description, 
            int maxSips = 3, bool isCarbonated = true, int pant = 1) 
            : base(calories, name, price, description)
        {
            this.IsCarbonated = isCarbonated;
            this.Pant = pant;
            this.MaxSips = maxSips;
        }

        /// <summary>
        /// Recycles the Soda-can. If Unusable, zero is returned. 
        /// Otherwise returns Pant and makes Unusable.
        /// </summary>
        /// <returns>Pant</returns>
        public int Recycle()
        {
            if (IsUnusable)
                return 0;

            IsUnusable = true;
            return Pant;
        }

        /// <summary>
        /// Returns string describing how Soda-can is used. If usable, increases number of sips taken. 
        /// If no more sips can be taken, the can is empty.
        /// </summary>
        /// <returns>String describing how can is used</returns>
        public override string Use()
        {
            if (IsUnusable) return "You do not know what to do with this.";

            if(Sips == MaxSips)
            {
                return $"You have finished the {Description} {Type} for {Calories} kcal and feel amazing";
            }

            Sips++;
            return $"You drink a sip of {Description} {Type}!";

        }
    }
}
