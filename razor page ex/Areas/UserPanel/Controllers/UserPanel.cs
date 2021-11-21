using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace razor_page_ex.Areas.UserPannel.Controllers
{
    [Area("UserPanel")]
    public class UserPanel : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
