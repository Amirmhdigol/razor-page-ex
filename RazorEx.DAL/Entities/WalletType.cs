using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorEx.DAL.Entities
{
    public class WalletType
    {
        public WalletType()
        {

        }

        [Key,DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TypeId { get; set; }
        public string TypeTitle { get; set; }

        public ICollection<Wallet> Wallet { get; set; }
    }
}
