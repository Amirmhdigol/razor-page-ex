using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using razor_page_ex.Areas.Adminstration.Models.ProductM;
using RazorEX.BAL.Contracts;
using RazorEX.BAL.DTOs.ProductDTOs;
using static RazorEX.BAL.DTOs.ProductDTOs.ProductFilterDTO;

namespace razor_page_ex.Areas.Adminstration.Controllers
{
    public class ProductController : AdminBase
    {
        private readonly IProduct _IProduct;

        public ProductController(IProduct iProduct)
        {
            _IProduct = iProduct;
        }

        public IActionResult Index(int PageId = 1, string Title = "", string Teacher = "")
        {
            var model = new ProductFilterParams()
            {
                Take = 2,
                PageId = PageId,
                Title = Title,
                Teacher = Teacher
            };
            return View(_IProduct.GetProductByFilter(model));
        }
        public IActionResult Add()
        {
            var a = _IProduct.GetStatuses();
            ViewData["Statuses"] = new SelectList(a, "Value", "Text");

            var b = _IProduct.GetLevels();
            ViewData["Levels"] = new SelectList(b, "Value", "Text");

            var c = _IProduct.GetUsers();
            ViewData["Teachers"] = new SelectList(c, "Value", "Text");

            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Add(AddProductViewModel productViewModel)
        {
            return View();
        }

        public IActionResult Edit()
        {
            return View();
        }

        //[HttpPost]
        //public IActionResult Edit()
        //{
        //    return View();
        //}
    }
}
