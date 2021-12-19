using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorEX.BAL.Contracts
{
    public interface IFileManager
    {
        public string SaveFile2(IFormFile file , string SavePath);
        public string SaveFile(IFormFile formFile, string SavePath);
        void DeleteFile(string fileName, string path);
    }
}
