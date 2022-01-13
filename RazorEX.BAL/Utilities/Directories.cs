using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorEX.BAL.Utilities
{
    public class Directories
    {
        public const string Post = "wwwroot/images/posts";
        public const string Products = "wwwroot/images/products";
        public const string ProductsDemo = "wwwroot/images/products/demoes";
        public static string GetPostImage(string imageName) => $"{Post.Replace("wwwroot", "")}/{imageName}";
        public static string GetProductImage(string imageName) => $"{Products.Replace("wwwroot", "")}/{imageName}";
    }
}
        