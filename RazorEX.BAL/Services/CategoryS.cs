using RazorEx.DAL.Context;
using RazorEX.BAL.Contracts;
using RazorEX.BAL.DTOs.CategoryDTO;
using RazorEX.BAL.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RazorEx.DAL.Entities;

namespace RazorEX.BAL.Services
{
    public class CategoryS : ICategory
    {
        private readonly RXContext _rXContext;

        public CategoryS(RXContext rXContext)
        {
            _rXContext = rXContext;
        }

        public OperationResult CreateCategory(CreateCategoryDto command)
        {
            if (IsSlugExist(command.Slug))
                return OperationResult.Error("Slug Is Exist");

            var category = new RazorEx.DAL.Entities.Category()
            {
                Title = command.Title,
                IsDelete = false,
                ParentId = command.ParentId,
                Slug = command.Slug,
                MetaTag = command.MetaTag,
                MetaDescription = command.MetaDescription,
            };
            _rXContext.Categories.Add(category);
            _rXContext.SaveChanges();
            return OperationResult.Success();
        }

        public OperationResult EditCategory(EditCategoryDto command)
        {
            var finded = _rXContext.Categories.FirstOrDefault(c => c.Id == command.Id);
            if (finded == null)
                return OperationResult.NotFound();

            if (command.Slug.ToSlug() != finded.Slug)
                if (IsSlugExist(command.Slug))
                    return OperationResult.Error("Slug Is Exist");

            finded.Title = command.Title;
            finded.MetaDescription = command.MetaDescription;
            finded.MetaTag = command.MetaTag;
            finded.Slug = command.Slug;

            _rXContext.SaveChanges();
            return OperationResult.Success();
        }

        public List<CategoryDto> GetAllCategory()
        {
            return _rXContext.Categories.Select(c => ToCategoryDTO.ToCatgoryDTO(c)).ToList();
        }

        public CategoryDto GetCategoryBy(int id)
        {
            var result = _rXContext.Categories.Find(id);

            if (result == null)
                return null;

            return ToCategoryDTO.ToCatgoryDTO(result);
        }

        public CategoryDto GetCategoryBy(string slug)
        {
            var res = _rXContext.Categories.FirstOrDefault(a => a.Slug == slug);

            if (res == null)
                return null;

            return ToCategoryDTO.ToCatgoryDTO(res);
        }

        public List<CategoryDto> GetChildCategory(int parentid)
        {
            return _rXContext.Categories.Where(n => n.ParentId == parentid)
                .Select(c => ToCategoryDTO.ToCatgoryDTO(c)).ToList();

        }

        public bool IsSlugExist(string slug)
        {
            return _rXContext.Categories.Any(c => c.Slug == slug.ToSlug());
        }
    }
}
