using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorEx.DAL.Entities
{
    public class UserDiscounts
    {
        [Key]
        public int UD_Id { get; set; }
        public int UserId { get; set; }
        public int DiscountId { get; set; }
        public User User { get; set; }
        public Discount Discount { get; set; }
    }
}
