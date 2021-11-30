using Microsoft.AspNetCore.Http;
using RazorEX.BAL.Contracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorEX.BAL.Services
{
    public class FileManager : IFileManager
    {
        public string SaveFile(IFormFile formFile, string SavePath)
        {
            if (formFile == null)
                throw new Exception("Null File");

            var FileName = $"{Guid.NewGuid()}{formFile.FileName}";
            var FolderPath = Path.Combine(Directory.GetCurrentDirectory(),SavePath.Replace("/","\\") );

            if (!Directory.Exists(FolderPath))
                Directory.CreateDirectory(FolderPath);

            var FullPath = Path.Combine(FolderPath , FileName);

            using var stream = new FileStream(FullPath,FileMode.Create);
            formFile.CopyTo(stream);
           
            return FileName;
        }
    }
}
