using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorEX.BAL.DTOs.Wallet
{
    public class WalletDTO
    {
        public int Amount { get; set; }
        public string Description { get; set; }
        public int TypeId { get; set; }
        public DateTime CreationDate { get; set; }
        public bool IsPay { get; set; }
        public int UserId { get; set; }
    }
}