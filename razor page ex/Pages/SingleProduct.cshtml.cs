using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorEX.BAL.Contracts;
using RazorEX.BAL.DTOs.ProductCommentDTOs;
using RazorEX.BAL.DTOs.ProductDTOs;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace razor_page_ex.Pages
{
    public class SingleProductModel : PageModel
    {
        private readonly IProduct _IProduct;
        private readonly IProductComment _productComment;

        public SingleProductModel(IProduct iProduct , IProductComment productComment)
        {
            _IProduct = iProduct;
            _productComment = productComment;
        }

        public ProductDTO Product { get; set; }
        public List<ProductDTO> RelatedProducts { get; set; }
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

            _IProduct.IncreaseVisit(Product.ProductId);
            return Page();
        }
    }
}
