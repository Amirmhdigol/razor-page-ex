using Microsoft.AspNetCore.Mvc;
using RazorEX.BAL.Contracts;

namespace razor_page_ex.Controllers
{
    public class Vote : Controller
    {
        private readonly IUser _user;
        private readonly IProduct _product;
        private readonly IOrder _order;

        public Vote(IUser user, IProduct product, IOrder order)
        {
            _user = user;
            _product = product;
            _order = order;
        }

        [Route("/Vote/{id}")]
        public IActionResult Index(int id)
        {
            if (!_product.IsFree(id))
            {
                if (!_product.IsUserBuyedThisProduct(User.Identity.Name, id))
                {
                    ViewBag.AccessDenied = true;
                }
            }
            return PartialView("ProductVote", _product.GetProductVotes(id));
        }
        [Route("/vote/addvote/{id}/{vote}")]
        public IActionResult AddVote(int id, bool vote)
        {
            int UserId = _user.GetUserIdByUserName(User.Identity.Name);
            _product.AddVote(id, UserId, vote);
            return PartialView("ProductVote", _product.GetProductVotes(id));
        }
    }
}
