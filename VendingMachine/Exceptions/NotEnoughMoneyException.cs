using System;
using System.Collections.Generic;
using System.Text;

namespace VendingMachine.Exceptions
{
    public class NotEnoughMoneyException : Exception
    {
        public NotEnoughMoneyException(string msg) : base(msg)
        {

        }
    }
}
