using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using razor_page_ex.Areas.Adminstration.Models;
using razor_page_ex.Areas.Adminstration.Models.CategoryM;
using RazorEx.DAL.Entities;
using RazorEX.BAL.Contracts;
using RazorEX.BAL.DTOs.CategoryDTO;
using RazorEX.BAL.Utilities;

namespace razor_page_ex.Areas.Adminstration.Controllers
{
    public class CategoryController : AdminBase
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

        [Route("/Adminstration/Category/Add/{ParentId?}")]
        public IActionResult Add(int? parentId)
        {
            return View();
        }

        [HttpPost("/Adminstration/Category/Add/{ParentId?}")]
        public IActionResult Add(int? ParentId ,CreateCategoryViewModel viewModel)
        {
            viewModel.ParentId = ParentId;
            var result = _categoryservice.CreateCategory(viewModel.Map());

            if (result.Status != OperationResultStatus.Success)
            {
                ModelState.AddModelError(nameof(viewModel.Slug), result.Message);
                return View();
            }
            return RedirectToAction("Index");
        }
        
        public IActionResult Edit(int id)
        {
            var Findedcategory = _categoryservice.GetCategoryBy(id);
            if (Findedcategory == null) return RedirectToAction("Index");
            var model = new EditCategoryViewModel()
            {
                Title = Findedcategory.Title,
                Slug = Findedcategory.Slug,
                MetaDescription = Findedcategory.MetaDescription,
                MetaTag = Findedcategory.MetaTag
            };
            return View(model: model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id , EditCategoryViewModel viewModel)
        {
            var result = _categoryservice.EditCategory(new EditCategoryDto()
            {
                Title = viewModel.Title,
                Slug = viewModel.Slug,
                MetaDescription = viewModel.MetaDescription,
                MetaTag = viewModel.MetaTag,
                Id = id
            });
            if (result.Status != OperationResultStatus.Success)
            {
                ModelState.AddModelError(nameof(viewModel.Slug), result.Message);
                return View();
            }
            return RedirectToAction("Index");
        }

        public IActionResult GetChildCategory(int parentid)
        {
            var category = _categoryservice.GetChildCategory(parentid);
            return new JsonResult(category);
        }
    }
}
