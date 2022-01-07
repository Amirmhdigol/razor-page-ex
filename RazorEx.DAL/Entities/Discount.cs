using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorEx.DAL.Entities
{
    public class Discount
    {
        public int DiscountId { get; set; }
        [MaxLength(150)]
        public string DiscountCode { get; set; }
        public int DiscountPercent { get; set; }
        public int? UsingCount { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public ICollection<UserDiscounts> UserDiscounts { get; set; } 
    }
}
