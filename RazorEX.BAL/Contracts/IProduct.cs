using Microsoft.AspNetCore.Mvc.Rendering;
using RazorEx.DAL.Entities;
using RazorEX.BAL.DTOs.ProductDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static RazorEX.BAL.DTOs.ProductDTOs.ProductFilterDTO;

namespace RazorEX.BAL.Contracts
{
    public interface IProduct
    {
        ShopPageDTO MainPageProducts();
        ProductFilterDTO GetProductByFilter(ProductFilterParams filterParams);
        List<SelectListItem> GetStatuses();
        List<SelectListItem> GetLevels();
        List<SelectListItem> GetUsers();
    }
}