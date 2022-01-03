using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorEX.BAL.DTOs.ProductDTOs
{
    public class AddProductDTO
    {
        public string Title { get; set; }
        public string Teacher { get; set; }
        public int Price { get; set; }
        public string Description { get; set; }
        public string Tags { get; set; }
        public IFormFile ProductImageName { get; set; }
        public IFormFile DemoFileName { get; set; }
        public int TeacherId { get; set; }
        public int CategoryId { get; set; }
        public int? SubCategoryId { get; set; }
        public int LevelId { get; set; }
        public int StatusId { get; set; }
    }
}
