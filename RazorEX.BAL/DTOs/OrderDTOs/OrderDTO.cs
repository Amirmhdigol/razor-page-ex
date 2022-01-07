using RazorEx.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorEX.BAL.DTOs.OrderDTOs
{
    public class OrderDTO
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public DateTime CreationDate { get; set; }
        public Int64 OrderPriceSum { get; set; }
        public bool IsFinally { get; set; }
        public UsersDTO.UserDTO User { get; set; }
        public ICollection<OrderDetail> OrderDetail { get; set; }
    }
}
