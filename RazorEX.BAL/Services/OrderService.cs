using Microsoft.EntityFrameworkCore;
using RazorEx.DAL.Context;
using RazorEx.DAL.Entities;
using RazorEX.BAL.Contracts;
using RazorEX.BAL.DTOs.OrderDTOs;
using RazorEX.BAL.DTOs.Wallet;
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

        public int AddOrder(string UserName, int ProductId)
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
            return order.OrderId;
        }

        public Order GetOrderById(int id)
        {
            return _context.Orders.Find(id);
        }

        public OrderDTO GetOrderForUserPanel(int OrderId, string Username)
        {
            int UserID = _user.GetUserIdByUserName(Username);

            Order FindedOrder = _context.Orders.Include(a => a.OrderDetail)
                .FirstOrDefault(a => a.UserId == UserID && a.OrderId == OrderId);
             
            return new OrderDTO()
            {
                CreationDate = FindedOrder.CreationDate,
                IsFinally = FindedOrder.IsFinally,
                OrderDetail = FindedOrder.OrderDetail,
                OrderPriceSum = FindedOrder.OrderPriceSum,
                OrderId = OrderId,
                UserId = UserID,
            };
        }

        public List<OrderDTO> GetUserOrders(string Username)
        {
            int userid = _user.GetUserIdByUserName(Username);
            var FindedOrders = _context.Orders.Include(a => a.User).Include(a => a.OrderDetail)
                .Where(a => a.UserId == userid && a.User.UserName == Username)
                .Select(a => new OrderDTO()
                {
                    CreationDate = a.CreationDate,
                    IsFinally = a.IsFinally,
                    OrderId = a.OrderId,
                    OrderPriceSum = a.OrderPriceSum,
                    UserId = a.UserId,
                }).ToList();
            return FindedOrders;
        }

        public bool SubmitOrder(string Username, int id)
        {
            int UserId = _user.GetUserIdByUserName(Username);
            var order = _context.Orders.Include(a => a.OrderDetail)
                           .ThenInclude(a => a.Products)
                           .FirstOrDefault(o => o.OrderId == id && o.UserId == UserId);

            if (order == null || order.IsFinally)
            {
                return false;
            }
            if (_user.UserWalletBalance(Username) >= order.OrderPriceSum)
            {
                order.IsFinally = true;
                _user.AddTransaction(new WalletDTO()
                {
                    Amount = order.OrderPriceSum,
                    CreationDate = DateTime.Now,
                    IsPay = true,
                    Description = "فاکتور شماره #" + order.OrderId,
                    UserId = UserId,
                    TypeId = 2,
                });
                _context.Orders.Update(order);
                foreach (var item in order.OrderDetail)
                {
                    _context.UserProducts.Add(new UserProducts()
                    {
                        ProductsId = item.ProductsId,
                        UserId = UserId,
                    });
                }
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public void UpdateOrder(Order order)
        {
            _context.Orders.Update(order);
            _context.SaveChanges();
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

        public DiscountUseType UseDiscount(int orderid, string Code)
        {
            var discount = _context.Discounts.SingleOrDefault(a => a.DiscountCode == Code);

            if (discount == null)
                return DiscountUseType.NotFound;

            if (discount.StartDate != null && discount.StartDate < DateTime.Now)
                return DiscountUseType.NotStarted;

            if (discount.EndDate != null && discount.EndDate >= DateTime.Now)
                return DiscountUseType.Expired;

            if (discount.UsingCount != null && discount.UsingCount < 1)
                return DiscountUseType.Expired;

            var order = GetOrderById(orderid);

            if (_context.UserDiscounts.Any(a => a.UserId == order.UserId && a.DiscountId == discount.DiscountId))
                return DiscountUseType.UserUsed;

            var percent = (order.OrderPriceSum * discount.DiscountPercent) / 100;
            order.OrderPriceSum -= percent;

            UpdateOrder(order);

            if (discount.UsingCount != null)
                discount.UsingCount -= 1;

            _context.Discounts.Update(discount);
            _context.UserDiscounts.Add(new UserDiscounts()
            {
                UserId = order.UserId,
                DiscountId = discount.DiscountId,
            });
            _context.SaveChanges();

            return DiscountUseType.Success;
        }
    }
}