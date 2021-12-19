using Microsoft.AspNetCore.Http;
using RazorEX.BAL.Contracts;
using RazorEX.BAL.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace RazorEX.BAL.Services
{
    public class FileManager : IFileManager
    {
        public void DeleteFile(string fileName, string path)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), path, fileName);
            if (File.Exists(filePath))
                File.Delete(filePath);
        }

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

        public string SaveFile2(IFormFile file , string FileSavepath)
        {
            if (file == null)
                throw new Exception("file is null");

            var isImage = file.Validate();

            if (!isImage)
                throw new Exception("Not an Image");

            return SaveFile(file, FileSavepath);
        }
    }
}