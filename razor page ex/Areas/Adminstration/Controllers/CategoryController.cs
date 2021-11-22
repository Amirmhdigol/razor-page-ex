using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using razor_page_ex.Areas.Adminstration.Models;
using RazorEx.DAL.Entities;
using RazorEX.BAL.Contracts;

namespace razor_page_ex.Areas.Adminstration.Controllers
{
    [Area("Adminstration")]
    public class CategoryController : Controller
    {
        private readonly ICategory _categoryservice;

        public CategoryController(ICategory category)
        {
            _categoryservice = category;
        }

        public IActionResult Index()
        {
            var Categories = _categoryservice.GetAllCategory();
            return View(model: Categories);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(CreateCategoryViewModel viewModel)
        {
            _categoryservice.CreateCategory(viewModel.Map());
            return RedirectToAction("Index");
        }
    }
}
