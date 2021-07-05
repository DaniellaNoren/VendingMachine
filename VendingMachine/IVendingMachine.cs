using System;
using System.Collections.Generic;
using System.Text;
using VendingMachine.Products;

namespace VendingMachine
{
    public interface IVendingMachine
    {
        public Product Purchase(int index);

        public ProductStock[] ShowAll();

        public void InsertMoney(int money);

        public Dictionary<int, int> EndTransaction();

    }
}
