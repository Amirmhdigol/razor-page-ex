using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RazorEx.DAL.Context;
using RazorEx.DAL.Entities;
using RazorEX.BAL.Contracts;
using RazorEX.BAL.DTOs.ProductDTOs;
using RazorEX.BAL.Utilities;
using RazorEX.BAL.Utilities.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorEX.BAL.Services
{
    public class ProductService : IProduct
    {
        private readonly IFileManager _fileManager;
        private readonly RXContext _context;
        public ProductService(RXContext context, IFileManager FileManager)
        {
            _context = context;
            _fileManager = FileManager;
        }

        public OperationResult AddProduct(AddProductDTO command)
        {
            if (command.ProductImageName == null)
                return OperationResult.Error();

            var product = new Products()
            {
                Title = command.Title,
                StatusId = command.StatusId,
                SubCategoryId = command.SubCategoryId == 0 ? null : command.SubCategoryId,
                CategoryId = command.CategoryId,
                CreationDate = DateTime.Now,
                Description = command.Description,
                IsDelete = false,
                Price = command.Price,
                LevelId = command.LevelId,
                TeacherId = command.TeacherId,
                Tags = command.Tags,
                ProductImageName = _fileManager.SaveFile2(command.ProductImageName, Directories.Products),
                DemoFileName = command.DemoFileName == null ? null :
                        _fileManager.SaveFile2(command.DemoFileName, Directories.Products)
            };
            _context.Products.Add(product);
            _context.SaveChanges();
            return OperationResult.Success();
        }

        public OperationResult DeleteProduct(int Id)
        {
            var FindedProduct = _context.Products.Find(Id);
            FindedProduct.IsDelete = true;
            return OperationResult.Success();
        }

        public OperationResult EditProduct(EditProductDTO command)
        {
            var FindedProduct = _context.Products.Include(a => a.Teacher)
                .Include(a => a.MainCategory)
                .FirstOrDefault(a => a.Id == command.ProductId);

            var oldImage = FindedProduct.ProductImageName;
            var oldDemoFile = FindedProduct.DemoFileName;

            if (FindedProduct == null)
                return OperationResult.NotFound();

            var NewTitle = command.Title;

            if (FindedProduct.Title != NewTitle)
                if (_context.Products.Any(a => a.Title == NewTitle))
                    return OperationResult.Error("عنوان تکراری است");

            FindedProduct.Title = command.Title;
            FindedProduct.Description = command.Description;
            FindedProduct.CategoryId = command.CategoryId;
            FindedProduct.Tags = command.Tags;
            FindedProduct.TeacherId = command.TeacherId;
            FindedProduct.Price = command.Price;
            FindedProduct.StatusId = command.StatusId;
            FindedProduct.Id = command.ProductId;
            FindedProduct.SubCategoryId = command.SubCategoryId == 0 ? null : command.SubCategoryId;
            FindedProduct.LevelId = command.LevelId;

            if (command.ProductImageName != null)
                FindedProduct.ProductImageName = _fileManager.SaveFile2(command.ProductImageName, Directories.Products);

            if (command.DemoFileName != null)
                FindedProduct.DemoFileName = _fileManager.SaveFile2(command.DemoFileName, Directories.Products);

            _context.Products.Update(FindedProduct);
            _context.SaveChanges();

            if (command.ProductImageName != null)
                _fileManager.DeleteFile(oldImage, Directories.Products);

            if (command.DemoFileName != null)
                _fileManager.DeleteFile(oldImage, Directories.Products);

            return OperationResult.Success();
        }

        public List<ProductDTO> GetDeletedProduct()
        {
            var product = _context.Products.IgnoreQueryFilters()
                                .OrderByDescending(a => a.CreationDate)
                                .Where(a => a.IsDelete)
                                .Select(a => new ProductDTO()
                                {
                                    Title = a.Title,
                                    CategoryId = a.CategoryId,
                                    CreationDate = a.CreationDate,
                                    DemoFileName = a.DemoFileName,
                                    Description = a.Description,
                                    IsDelete = a.IsDelete,
                                    LevelId = a.LevelId,
                                    StatusId = a.StatusId,
                                    Price = a.Price,
                                    Teacher = a.Teacher.UserName,
                                    CategorySlug = a.MainCategory.Slug,
                                    Category = a.MainCategory.Title,
                                    //ProductEpisodes = product.ProductEpisodes == null ? null : ProductEpisodetoDTO.Map(a.ProductEpisodes),
                                    ProductId = a.Id,
                                    ProductImageName = a.ProductImageName,
                                    Tags = a.Tags,
                                    SubCategoryId = a.SubCategoryId,
                                    TeacherId = a.TeacherId
                                }).ToList();
            return product;
        }

        public List<SelectListItem> GetLevels()
        {
            return _context.ProductLevels.Select(l => new SelectListItem()
            {
                Value = l.LevelId.ToString(),
                Text = l.LevelTitle
            }).ToList();
        }

        public ProductFilterDTO GetProductByFilter(ProductFilterDTO.ProductFilterParams filterParams)
        {
            var Result = _context.Products
                .Include(n => n.Teacher)
                .Include(a => a.MainCategory)
                .OrderByDescending(p => p.CreationDate)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(filterParams.Teacher))
                Result = Result.Where(a => a.Teacher.UserName.Contains(filterParams.Teacher));

            if (!string.IsNullOrWhiteSpace(filterParams.Title))
                Result = Result.Where(a => a.Title.Contains(filterParams.Title));

            var skip = (filterParams.PageId - 1) * filterParams.Take;
            var model = new ProductFilterDTO()
            {
                Products = Result.Skip(skip).Take(filterParams.Take)
                                .Select(product => new ProductDTO()
                                {
                                    Title = product.Title,
                                    CategoryId = product.CategoryId,
                                    CreationDate = product.CreationDate,
                                    DemoFileName = product.DemoFileName,
                                    Description = product.Description,
                                    IsDelete = product.IsDelete,
                                    LevelId = product.LevelId,
                                    StatusId = product.StatusId,
                                    Price = product.Price,
                                    Teacher = product.Teacher.UserName,
                                    CategorySlug = product.MainCategory.Slug,
                                    Category = product.MainCategory.Title,
                                    //ProductEpisodes = product.ProductEpisodes == null ? null : ProductEpisodetoDTO.Map(a.ProductEpisodes),
                                    ProductId = product.Id,
                                    ProductImageName = product.ProductImageName,
                                    Tags = product.Tags,
                                    SubCategoryId = product.SubCategoryId,
                                    TeacherId = product.TeacherId
                                }).ToList(),
                FilterParams = filterParams,
            };
            model.GeneratePaging(Result, filterParams.Take, filterParams.PageId);

            return model;
        }

        public ProductDTO GetProductById(int Id)
        {
            var FindedProduct = _context.Products.Include(a => a.Teacher)
                .Include(a => a.MainCategory)
                .FirstOrDefault(a => a.Id == Id);

            if (FindedProduct == null)
                return null;

            return new ProductDTO()
            {
                Title = FindedProduct.Title,
                CategoryId = FindedProduct.CategoryId,
                CreationDate = FindedProduct.CreationDate,
                DemoFileName = FindedProduct.DemoFileName,
                Description = FindedProduct.Description,
                IsDelete = FindedProduct.IsDelete,
                LevelId = FindedProduct.LevelId,
                StatusId = FindedProduct.StatusId,
                Price = FindedProduct.Price,
                Teacher = FindedProduct.Teacher.UserName,
                CategorySlug = FindedProduct.MainCategory.Slug,
                Category = FindedProduct.MainCategory.Title,
                //ProductEpisodes = product.ProductEpisodes == null ? null : ProductEpisodetoDTO.Map(a.ProductEpisodes),
                ProductId = FindedProduct.Id,
                ProductImageName = FindedProduct.ProductImageName,
                Tags = FindedProduct.Tags,
                SubCategoryId = FindedProduct.SubCategoryId,
                TeacherId = FindedProduct.TeacherId
            };
        }

        public ProductDTO GetProductByTitle(string Title)
        {
            if(string.IsNullOrEmpty(Title))
                return null;

            var FindedProduct = _context.Products
                .Include(a=>a.MainCategory)
                .Include(a=>a.Teacher)
                .Include(a=>a.SubCategory)
                .OrderByDescending(a=>a.CreationDate)
                .FirstOrDefault(a=>a.Title==Title);

            var ProductDTO = new ProductDTO()
            {
                Title = FindedProduct.Title,
                CategoryId = FindedProduct.CategoryId,
                CreationDate = FindedProduct.CreationDate,
                DemoFileName = FindedProduct.DemoFileName,
                Description = FindedProduct.Description,
                IsDelete = FindedProduct.IsDelete,
                LevelId = FindedProduct.LevelId,
                Visit = FindedProduct.Visit,
                StatusId = FindedProduct.StatusId,
                Price = FindedProduct.Price,
                Teacher = FindedProduct.Teacher.UserName,
                CategorySlug = FindedProduct.MainCategory.Slug,
                Category = FindedProduct.MainCategory.Title,
                //ProductEpisodes = product.ProductEpisodes == null ? null : ProductEpisodetoDTO.Map(a.ProductEpisodes),
                ProductId = FindedProduct.Id,
                ProductImageName = FindedProduct.ProductImageName,
                Tags = FindedProduct.Tags,
                SubCategoryId = FindedProduct.SubCategoryId,
                TeacherId = FindedProduct.TeacherId
            };
            return ProductDTO;
        }

        public List<ProductDTO> GetRelatedProducts(int CategoryId)
        {
            var FindedProduct = _context.Products
                .Include(a => a.MainCategory)
                .Include(a => a.Teacher)
                .Include(a => a.SubCategory)
                .Where(a => a.CategoryId == CategoryId || a.SubCategoryId == CategoryId)
                .OrderByDescending(a => a.CreationDate)
                .Take(6)
                .Select(a => new ProductDTO()
                {
                    Title = a.Title,
                    CategoryId = a.CategoryId,
                    CreationDate = a.CreationDate,
                    DemoFileName = a.DemoFileName,
                    Description = a.Description,
                    IsDelete = a.IsDelete,
                    LevelId = a.LevelId,
                    StatusId = a.StatusId,
                    Price = a.Price,
                    Teacher = a.Teacher.UserName,
                    CategorySlug = a.MainCategory.Slug,
                    Category = a.MainCategory.Title,
                    //ProductEpisodes = a.ProductEpisodes == null ? null : ProductEpisodetoDTO.Map(a.ProductEpisodes),
                    ProductId = a.Id,
                    ProductImageName = a.ProductImageName,
                    Tags = a.Tags,
                    SubCategoryId = a.SubCategoryId,
                    TeacherId = a.TeacherId

                }).ToList();

            return FindedProduct;
        }

        public List<SelectListItem> GetStatuses()
        {
            return _context.ProductStatuses.Select(l => new SelectListItem()
            {
                Value = l.StatusId.ToString(),
                Text = l.StatusTitle
            }).ToList();
        }

        public List<SelectListItem> GetUsers()
        {
            return _context.Users.Select(l => new SelectListItem()
            {
                Value = l.Id.ToString(),
                Text = l.UserName
            }).ToList();
        }

        public ShopPageDTO MainPageProducts()
        {
            var Products = _context.Products
               .Include(a => a.Teacher)
               .Include(a => a.MainCategory)
               .Include(a => a.SubCategory)
               .Include(a => a.ProductEpisodes)
               .OrderByDescending(a => a.CreationDate)
               .Take(15)
               .Select(a => new ProductDTO()
               {
                   Title = a.Title,
                   CategoryId = a.CategoryId,
                   CreationDate = a.CreationDate,
                   DemoFileName = a.DemoFileName,
                   Description = a.Description,
                   IsDelete = a.IsDelete,
                   LevelId = a.LevelId,
                   StatusId = a.StatusId,
                   Price = a.Price,
                   Teacher = a.Teacher.UserName,
                   CategorySlug = a.MainCategory.Slug,
                   Category = a.MainCategory.Title,
                   SubCategory = a.SubCategory.Title,
                   SubCategorySlug = a.SubCategory.Slug,
                   //ProductEpisodes = a.ProductEpisodes == null ? null : ProductEpisodetoDTO.Map(a.ProductEpisodes),
                   ProductId = a.Id,
                   ProductImageName = a.ProductImageName,
                   Tags = a.Tags,
                   SubCategoryId = a.SubCategoryId,
                   TeacherId = a.TeacherId
               }).ToList();

            return new ShopPageDTO()
            {
                AllProducts = Products,
            };

        }
        public void IncreaseVisit(int ProductID)
        {
            var Product = _context.Products.FirstOrDefault(a => a.Id == ProductID);
            Product.Visit += 1;
            _context.SaveChanges();
        }
    }
}