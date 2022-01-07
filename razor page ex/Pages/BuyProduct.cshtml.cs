using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorEX.BAL.Contracts;
using RazorEX.BAL.DTOs.ProductDTOs;

namespace razor_page_ex.Pages
{
    [Authorize]
    public class BuyProductModel : PageModel
    {
        private readonly IProduct _IProduct;
        private readonly IOrder _order;

        public BuyProductModel(IOrder order, IProduct iProduct)
        {
            _order = order;
            _IProduct = iProduct;
        }
        public ProductDTO Product { get; set; }

        public IActionResult OnGet(int id,string Title)
        {
            var FindedProduct = _IProduct.GetProductByTitle(Title);

            if (FindedProduct == null)
                return NotFound();

            Product = FindedProduct;
                
            int orderid = _order.AddOrder(User.Identity.Name, id);
            return Redirect("/Userpanel/myorder/Showorder/" + orderid);
        }
    }
}
