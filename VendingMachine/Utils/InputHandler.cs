using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace VendingMachine
{
    public class InputHandler
    {
        /// <summary>
        /// Gets input from console. If it is a valid number, it returns it. Otherwise exception is thrown.
        /// </summary>
        /// <returns></returns>
        public static int GetValidNumber()
        {
            string input = GetInput();
            bool validInput = Int32.TryParse(input, out int nr);

            if (validInput)
                return nr;
            else
                throw new ArgumentException("Invalid number.");
        }

        /// <summary>
        /// Gets input from user. If exception is thrown, returns empty string.
        /// </summary>
        /// <returns>String input from user</returns>
        public static string GetInput()
        {
            try
            {
                return Console.ReadLine();
            }catch(IOException)
            {
                return "";
            }catch (OutOfMemoryException)
            {
                return "";
            }
            catch (ArgumentOutOfRangeException)
            {
                return "";
            }
        }
    }
}
