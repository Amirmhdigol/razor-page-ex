using Microsoft.AspNetCore.Mvc;

namespace razor_page_ex.Areas.Adminstration.Controllers
{
    public class UserController : AdminBase
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
