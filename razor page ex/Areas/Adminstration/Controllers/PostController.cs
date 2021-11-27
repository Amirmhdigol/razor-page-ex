using Microsoft.AspNetCore.Mvc;
using RazorEX.BAL.Contracts;
using RazorEX.BAL.DTOs.PostDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace razor_page_ex.Areas.Adminstration.Controllers
{
    [Area("Adminstration")]
    public class PostController : Controller
    {
        private readonly IPost _context;

        public PostController(IPost context)
        {
            _context = context;
        }

        public IActionResult Index(int PageId = 1, string Title = "", string CategorySlug = "")
        {
            var model = _context.GetPostByFilter(new PostFilterParams() 
            {
                Title = Title,
                CategorySlug = CategorySlug,
                PageId = PageId,
                Take = 20,
            });
            return View(model : model);
        } 
    }
}
