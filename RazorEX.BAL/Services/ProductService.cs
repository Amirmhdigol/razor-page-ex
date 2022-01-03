using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RazorEx.DAL.Context;
using RazorEx.DAL.Entities;
using RazorEX.BAL.Contracts;
using RazorEX.BAL.DTOs.ProductDTOs;
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
        private readonly RXContext _context;
        public ProductService(RXContext context)
        {
            _context = context;
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

            return new ShopPageDTO()
            {
                AllProducts = Products,
            };
        }
    }
}
