using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorEx.DAL.Entities;
using RazorEX.BAL.Contracts;
using RazorEX.BAL.DTOs.ProductDTOs;

namespace razor_page_ex.Pages
{
    public class ShopModel : PageModel
    {
        private readonly IProduct _product;

        public ShopModel(IProduct product)
        {
            _product = product;
        }
        public ShopPageDTO MainPageData;
        public void OnGet()
        {
            MainPageData =  _product.MainPageProducts();
        }
    }
}
