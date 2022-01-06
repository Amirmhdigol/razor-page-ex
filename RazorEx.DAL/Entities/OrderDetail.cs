using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorEx.DAL.Entities
{
    public class OrderDetail
    {
        public int OrderDetailId { get; set; }
        [Required]
        public int ProductsId { get; set; }
        [Required]
        public int OrderId { get; set; }
        [Required]
        public int Count { get; set; }
        [Required]
        public Int64 Price { get; set; }

        public Products Products { get; set; }
        public Order Order { get; set; }
    }
}
