using System;
using System.Collections.Generic;
using System.Text;

namespace VendingMachine
{
    public class InputHandler
    {
        public static int GetValidNumber()
        {
            string input = GetInput();
            bool validInput = Int32.TryParse(input, out int nr);

            if (validInput)
                return nr;
            else
                throw new ArgumentException("Invalid number.");
        }

        private static string GetInput()
        {
            try
            {
                return Console.ReadLine();
            }catch(Exception)
            {
                return "";
            }
        }
    }
}
