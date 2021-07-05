using System;

namespace VendingMachine.Products
{
    public class OutOfStockException : Exception
    {
        public OutOfStockException(string msg) : base(msg)
        {

        }
    }
}
