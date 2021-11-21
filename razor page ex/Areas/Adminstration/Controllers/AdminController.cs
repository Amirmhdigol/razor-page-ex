using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace razor_page_ex.Areas.Adminstration.Controllers
{
    public class AdminController : Controller
    {
        [Area("Adminstration")]
        public IActionResult Index()
        {
            var a = "amir";
            var b = "Mahdi";
            var c = a + b;
            
            return View(model: c);
        }
    }
}
