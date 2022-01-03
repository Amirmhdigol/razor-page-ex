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
            var product = new AddProductDTO()
            {
                CategoryId = productViewModel.CategoryId,
                StatusId = productViewModel.StatusId,
                SubCategoryId = productViewModel.SubCategoryId,
                Title = productViewModel.Title,
                DemoFileName = productViewModel.DemoFileName,
                Description = productViewModel.Description,
                LevelId = productViewModel.LevelId,
                Price = productViewModel.Price,
                ProductImageName = productViewModel.ProductImageName,
                Tags = productViewModel.Tags,
                TeacherId = productViewModel.TeacherId,
            };
            _IProduct.AddProduct(product);
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int Id)
        {
            var a = _IProduct.GetStatuses();
            ViewData["Statuses"] = new SelectList(a, "Value", "Text");

            var b = _IProduct.GetLevels();
            ViewData["Levels"] = new SelectList(b, "Value", "Text");

            var c = _IProduct.GetUsers();
            ViewData["Teachers"] = new SelectList(c, "Value", "Text");

            var model = _IProduct.GetProductById(Id);
            var model1 = new EditProductViewModel()
            {
                CategoryId = model.CategoryId,
                StatusId = model.StatusId,
                SubCategoryId = model.SubCategoryId,
                LevelId = model.LevelId,
                Description = model.Description,
                Price = model.Price,
                Tags = model.Tags,
                Title = model.Title,
                TeacherId = model.TeacherId,
            };
            return View(model1);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Edit(int Id , EditProductViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View(viewModel);
           _IProduct.EditProduct(new EditProductDTO()
           {
               ProductId = Id,
               CategoryId =viewModel.CategoryId,
               Description=viewModel.Description,
               LevelId=viewModel.LevelId,
               DemoFileName = viewModel.DemoFileName,
               Price = viewModel.Price,
               ProductImageName = viewModel.ProductImageName,
               StatusId=viewModel.StatusId,
               SubCategoryId=viewModel.SubCategoryId,
               Tags = viewModel.Tags,
               TeacherId=viewModel.TeacherId,
               Title = viewModel.Title,
           });
            return RedirectToAction("Index");
        }

        public void Delete(int Id)
        {
            _IProduct.DeleteProduct(Id);
        }

        public IActionResult DeletedProducts()
        {
            var model = _IProduct.GetDeletedProduct();
            return View(model);
        }
    }
}
