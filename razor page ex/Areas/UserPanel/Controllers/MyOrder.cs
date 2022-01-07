using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RazorEX.BAL.Contracts;

namespace razor_page_ex.Areas.UserPanel.Controllers
{
    [Authorize]
    [Area("UserPanel")]
    public class MyOrder : Controller
    {
        private readonly IOrder _order;

        public MyOrder(IOrder order)
        {
            _order = order;
        }

        public IActionResult Index()
        {
            var Model = _order.GetUserOrders(User.Identity.Name);
            return View(Model);
        }

        public IActionResult ShowOrder(int id, bool finaly = false , string type="")
        {
            var FindedOrder = _order.GetOrderForUserPanel(id, User.Identity.Name);
            if (FindedOrder == null)
                return NotFound();

            ViewBag.typeDiscount = type;
            ViewBag.finaly = finaly;
            return View(FindedOrder);
        }
        public IActionResult SubmitOrder(int id)
        {
            if (_order.SubmitOrder(User.Identity.Name, id))
            {
                return Redirect("/UserPanel/MyOrder/ShowOrder/" + id + "?finaly=true");
            }
            return BadRequest();
        }
        public IActionResult UseDiscount(int orderid , string code)
        {
            var type = _order.UseDiscount(orderid, code);
            return Redirect("/UserPanel/myorder/showorder/" + orderid +"?type="+type.ToString());
        }
    }
}
