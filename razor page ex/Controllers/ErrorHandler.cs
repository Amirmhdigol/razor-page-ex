using Microsoft.AspNetCore.Mvc;

namespace razor_page_ex.Controllers
{
    public class ErrorHandler : Controller
    {
        [Route("/ErrorHandler/{code}")]
        public IActionResult Index(int code)
        {
            switch (code)
            {
                case 404:
                    return View("NotFound");

                case 500:
                    return View("ServerException");
            }
            return View("NotFound");
        }
    }
}   
