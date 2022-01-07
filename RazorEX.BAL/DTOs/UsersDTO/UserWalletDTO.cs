using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorEX.BAL.DTOs.UsersDTO
{
    public class UserWalletDTO
    {
        public Int64 Amount { get; set; }
        public int Type { get; set; }
        public string Description { get; set; }
        public DateTime TransactionDate { get; set; }
    }
}
