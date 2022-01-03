using RazorEX.BAL.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorEX.BAL.DTOs.ProductDTOs
{
    public class ProductFilterDTO : BasePagination
    {
        public List<ProductDTO> Products { get; set; }
        public ProductFilterParams FilterParams { get; set; }

        public class ProductFilterParams
        {
            public int Take { get; set; }
            public int PageId { get; set; }
            public string Title { get; set; }
            public string Teacher { get; set; }
        }
    }
}
