using RazorEx.DAL.Entities;
using RazorEX.BAL.DTOs.OrderDTOs;
using RazorEX.BAL.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorEX.BAL.Contracts
{
    public interface IOrder
    {
        int AddOrder(string UserName, int ProductId);
        void UpdatePriceOrder(int ProductId);
        void UpdateOrder(Order order);
        OrderDTO GetOrderForUserPanel(int OrderId, string Username);
        Order GetOrderById(int id);
        List<OrderDTO> GetUserOrders(string Username);
        DiscountUseType UseDiscount(int orderid, string Code);
        bool SubmitOrder(string Username, int id);
    }
}
