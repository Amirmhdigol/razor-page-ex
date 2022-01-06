using RazorEx.DAL.Context;
using RazorEx.DAL.Entities;
using RazorEX.BAL.Contracts;
using RazorEX.BAL.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorEX.BAL.Services
{
    public class OrderService : IOrder
    {
        private readonly RXContext _context;
        private readonly IUser _user;

        public OrderService(IUser user, RXContext rXContext)
        {
            _user = user;
            _context = rXContext;
        }

        public OperationResult AddOrder(string UserName, int ProductId)
        {
            int UserId = _user.GetUserIdByUserName(UserName);

            Order order = _context.Orders.
                FirstOrDefault(a => a.UserId == UserId && !a.IsFinally);

            Products Product = _context.Products.Find(ProductId);

            if (order == null)
            {
                order = new Order()
                {
                    UserId = UserId,
                    IsFinally = false,
                    CreationDate = DateTime.Now,
                    OrderPriceSum = Product.Price,
                    OrderDetail = new List<OrderDetail>()
                    {
                        new OrderDetail()
                        {
                            ProductsId = ProductId,
                            Count = 1,
                            Price = Product.Price,
                        }
                    }
                };
                _context.Orders.Add(order);
                _context.SaveChanges();
            }
            else
            {
                OrderDetail orderDetail = _context.OrderDetails
                    .FirstOrDefault(a => a.OrderId == order.OrderId && a.ProductsId == ProductId);

                if (orderDetail != null)
                {
                    orderDetail.Count += 1;
                    _context.OrderDetails.Update(orderDetail);
                    _context.SaveChanges();
                }
                else
                {
                    orderDetail = new OrderDetail()
                    {
                        OrderId = order.OrderId,
                        Count = 1,
                        ProductsId = ProductId,
                        Price = Product.Price,
                    };
                    _context.OrderDetails.Add(orderDetail);
                    _context.SaveChanges();
                }
                UpdatePriceOrder(order.OrderId);
                _context.SaveChanges();
            }
            return OperationResult.Success();
        }

        public void UpdatePriceOrder(int OrderId)
        {
            var order = _context.Orders.Find(OrderId);
            var SameOrder = _context.OrderDetails.Where(a => a.OrderId == OrderId && a.Count == 1).ToList();
            var SameOrder2 = _context.OrderDetails.Where(a => a.OrderId == OrderId && a.Count > 1).ToList();

            var SumPrice = SameOrder.Sum(a => a.Price);
            var SumPrice2 = SameOrder2.Sum(a => a.Price * a.Count);
            var AllSummedPrice = SumPrice + SumPrice2;  

            //order.OrderPriceSum = AllSummedPrice;
            //_context.Orders.Update(order);
            order.OrderPriceSum = AllSummedPrice;
            _context.SaveChanges();
        }
    }
}