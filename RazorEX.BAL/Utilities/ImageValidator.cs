using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorEX.BAL.Utilities
{
    public static class ImageValidator
    {
        public static bool Validate(this IFormFile file)
        {
            if (file == null)
                return false;
            try
            {
                var Image = System.Drawing.Image.FromStream(file.OpenReadStream());
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool Validate(this string Filename)
        {
            if (Filename == null)
                return false;

            var Extension = Path.GetExtension(Filename);
            
            if (Extension.ToLower() == ".png" || Extension.ToLower() == ".jpg" || Extension.ToLower() == ".jpeg")
                return true;
            
            return false;
        }
    }
}
