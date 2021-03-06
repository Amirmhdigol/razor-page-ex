using Microsoft.AspNetCore.Mvc.Rendering;
using RazorEx.DAL.Entities;
using RazorEX.BAL.DTOs.ProductDTOs;
using RazorEX.BAL.Utilities;
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
        OperationResult AddProduct(AddProductDTO command);
        OperationResult EditProduct(EditProductDTO command);
        OperationResult DeleteProduct(int Id);
        List<ProductDTO> GetDeletedProduct();
        ProductDTO GetProductByTitle(string Title);
        ProductDTO GetProductById(int Id);
        ProductFilterDTO GetProductByFilter(ProductFilterParams filterParams);
        List<ProductDTO> GetRelatedProducts(int CategoryId);
        List<ProductDTO> GetPopularProducts();
        List<SelectListItem> GetStatuses();
        List<SelectListItem> GetLevels();
        List<SelectListItem> GetUsers();
        public void IncreaseVisit(int ProductID);
        public bool IsUserBuyedThisProduct(string UserName , int ProductsId);
        public void AddVote(int productId, int userId, bool vote);
        Tuple<int, int> GetProductVotes(int productId);
        bool IsFree(int productId);
    }
}