using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorEX.BAL.Contracts;
using RazorEX.BAL.DTOs.ProductCommentDTOs;
using RazorEX.BAL.DTOs.ProductDTOs;
using RazorEX.BAL.Utilities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace razor_page_ex.Pages
{
    public class SingleProductModel : PageModel
    {
        private readonly IProduct _IProduct;
        private readonly IProductComment _productComment;
        private readonly IOrder _order;

        public SingleProductModel(IProduct iProduct, IProductComment productComment, IOrder order)
        {
            _order = order;
            _IProduct = iProduct;
            _productComment = productComment;
        }

        public ProductDTO Product { get; set; }
        public List<ProductDTO> RelatedProducts { get; set; }
        public List<ProductDTO> PopularProducts { get; set; }
        public List<ProductCommentDTO> ProductComments { get; set; }

        [Required]
        [BindProperty]
        public string Text { get; set; }
        [BindProperty]
        public int ProductId { get; set; }

        public IActionResult OnGet(string Title)
        {
            var FindedProduct = _IProduct.GetProductByTitle(Title);

            if (FindedProduct == null)
                return NotFound();

            Product = FindedProduct;

            ProductComments = _productComment.GetProductComments(Product.ProductId);

            var FindedRelatedProducts = _IProduct.GetRelatedProducts(Product.SubCategoryId ?? Product.CategoryId);
            RelatedProducts = FindedRelatedProducts;
            PopularProducts = _IProduct.GetPopularProducts();

            _IProduct.IncreaseVisit(Product.ProductId);
            return Page();
        }
        public IActionResult OnPost(string Title)
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToPage("SingleProduct", new { Title });

            if (!ModelState.IsValid)
            {
                var FindedProduct = _IProduct.GetProductByTitle(Title);
                Product = FindedProduct;
                ProductComments = _productComment.GetProductComments(Product.ProductId);
                var FindedRelatedProducts = _IProduct.GetRelatedProducts(Product.SubCategoryId ?? Product.CategoryId);
                RelatedProducts = FindedRelatedProducts;
                return Page();
            }
            _productComment.CreateProductComment(new CreateProductCommentDTO()
            {
                ProductId = ProductId,
                Text = Text,
                UserId = User.Getid()
            });
            return RedirectToPage("SingleProduct", new { Title });
        }
        public IActionResult OnGetBuyProduct(int id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Redirect("Register/Signup");
            }
            _order.AddOrder(User.Identity.Name, id);
            return Redirect("/SingleProduct/" + ProductId);
        }
        public IActionResult OnGetPopularProducts()
        {
            return Partial("_PopularProducts", _IProduct.GetPopularProducts());
        }
    }
}