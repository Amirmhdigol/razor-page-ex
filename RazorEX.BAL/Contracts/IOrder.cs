using RazorEX.BAL.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorEX.BAL.Contracts
{
    public interface IOrder
    {
        OperationResult AddOrder(string UserName, int ProductId);

        void UpdatePriceOrder(int ProductId);
    }
}
