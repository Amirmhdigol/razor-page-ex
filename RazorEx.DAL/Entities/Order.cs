using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorEx.DAL.Entities
{
    public class Order
    {
        public int OrderId { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public DateTime CreationDate { get; set; }
        [Required]
        public Int64 OrderPriceSum { get; set; }
        public bool IsFinally { get; set; }

        public User User { get; set; }
        public ICollection<OrderDetail> OrderDetail { get; set; }
    }
}
