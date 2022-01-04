using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static RazorEx.DAL.Entities.Products;

namespace RazorEX.BAL.DTOs.ProductDTOs
{
    public class ProductDTO
    {
        public int ProductId { get; set; }
        public bool IsDelete { get; set; }
        public DateTime CreationDate { get; set; }
        public int Visit { get; set; }
        public string Title { get; set; }
        public ProductEpisodesDTO ProductEpisodes { get; set; }
        public string Teacher { get; set; }
        public string Category { get; set; }
        public string CategorySlug { get; set; }
        public string SubCategory { get; set; }
        public string SubCategorySlug { get; set; }
        public int Price { get; set; }
        public string Description { get; set; }
        public string Tags { get; set; }
        public string ProductImageName { get; set; }
        public string DemoFileName { get; set; }
        public int TeacherId { get; set; }
        public int CategoryId { get; set; }
        public int? SubCategoryId { get; set; }
        public int LevelId { get; set; }
        public int StatusId { get; set; }
    }
}
