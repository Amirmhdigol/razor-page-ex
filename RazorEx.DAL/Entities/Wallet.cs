using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace RazorEx.DAL.Entities
{
    public class Wallet:BaseEntity
    {
        public Wallet()
        {

        }
        [ForeignKey("WalletType")]
        public int TypeId { get; set; }
        public int UserId { get; set; }
        public Int64 Amount { get; set; }
        public string Description { get; set; }
        public bool IsPay { get; set; }

        #region Relations
        public virtual User User { get; set; }
        public virtual WalletType WalletType { get; set; }
        #endregion
    }
}
