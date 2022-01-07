using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorEx.DAL.Entities
{
    public class UserProducts
    {
        public int UserProductsId { get; set; }
        public int ProductsId { get; set; }
        public int UserId { get; set; }

        public Products product { get; set; }
        public User User { get; set; }
    }
}

