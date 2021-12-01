using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using razor_page_ex.Areas.Adminstration.Models.PostsM;
using RazorEX.BAL.Contracts;
using RazorEX.BAL.DTOs.PostDTO;
using RazorEX.BAL.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace razor_page_ex.Areas.Adminstration.Controllers
{
    [Area("Adminstration")]
    [Authorize]
    public class PostController : Controller
    {
        private readonly IPost _context;

        public PostController(IPost context)
        {
            _context = context;
        }

        public IActionResult Index(int PageId = 1, string Title = "", string CategorySlug = "")
        {
            var model =new PostFilterParams()
            {
                Title = Title,
                CategorySlug = CategorySlug,
                PageId = PageId,
                Take = 10,
            };
            var model1 = _context.GetPostByFilter(model);
            return View(model: model1);
        }

        public IActionResult Add()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Add(CreatepostViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View();

            var Result = _context.CreatePost(new CreatePostDTO()
            {
                UserId = User.Getid(),
                Title = viewModel.Title,
                Description = viewModel.Description,
                Slug = viewModel.Slug,
                SubCategoryId = viewModel.SubCategoryId == 0 ? null : viewModel.SubCategoryId,
                CategoryId = viewModel.CategoryId,
                ImageFile = viewModel.ImageFile
            });

            if (Result.Status != OperationResultStatus.Success)
                return View();

            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var FindedPost = _context.GetPostById(id);
            if (FindedPost == null)
                return RedirectToAction("Index");

            var model1 = new EditPostViewModel()
            {
                CategoryId = FindedPost.CategoryId,
                Description = FindedPost.Description,
                Slug = FindedPost.Slug,
                SubCategoryId = FindedPost.SubCategoryId,
                Title = FindedPost.Title,
            };

            return View(model: model1);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, EditPostViewModel edit)
        {
            if (!ModelState.IsValid)
                return View(model:edit);

            var result = _context.EditPost(new EditPostDTO()
            {
                CategoryId = edit.CategoryId,
                Description = edit.Description,
                ImageFile = edit.ImageFile,
                Slug = edit.Slug,
                SubCategoryId = edit.SubCategoryId == 0 ? null : edit.SubCategoryId,
                Title = edit.Title,
                PostId = id
            });

            if (result.Status != OperationResultStatus.Success)
            {
                ModelState.AddModelError(nameof(CreatepostViewModel.Slug), result.Message);
                return View(model:edit);
            }

            return RedirectToAction("Index");
        }
    }
}
