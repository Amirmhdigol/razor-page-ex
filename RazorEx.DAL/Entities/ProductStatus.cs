using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorEx.DAL.Entities
{
    public class ProductStatus
    {
        [Key]
        public int StatusId { get; set; }

        [MaxLength(150)]
        [Required]
        public string StatusTitle { get; set; }
        public List<Products> Products { get; set; }
    }
}
