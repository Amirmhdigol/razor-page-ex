using Microsoft.EntityFrameworkCore;
using RazorEx.DAL.Context;
using RazorEX.BAL.DTOs.MainPageDTO;
using RazorEX.BAL.Utilities.Mapper;
using System.Linq;

namespace RazorEX.BAL.Contracts
{
    public class MainPage : IMainPage
    {
        private readonly RXContext _rXContext;

        public MainPage(RXContext rXContext)
        {
            _rXContext = rXContext;
        }

        public PageDTO GetData()
        {
            var LatestPosts = _rXContext.Posts
                .Include(a => a.Category)
                .Include(a => a.SubCategory)
                .Include(a => a.User)
                .OrderByDescending(a => a.Id)
                .Take(6).Select(a => FindPostMapper.Map(a)).ToList();

            var SpecialPosts = _rXContext.Posts
                .Include(a => a.Category)
                .Include(a => a.SubCategory)
                .Include(a => a.User)
                .OrderByDescending(a => a.Id)
                .Where(a => a.IsSpecial)
                .Take(6).Select(a => FindPostMapper.Map(a)).ToList();

            var Categories = _rXContext.Categories
                .Include(a => a.Posts)
                .Include(a => a.SubPosts)
                .OrderByDescending(a => a.Id)
                .Select(a => new MainPageCategoryDTO()
                {
                    Title = a.Title,
                    Slug = a.Slug,
                    PostChild = a.Posts.Count + a.SubPosts.Count,
                    IsMainCategory = a.ParentId == null
                })
                .ToList();

            return new PageDTO()
            {
                LatestPosts = LatestPosts,
                SpecialPosts = SpecialPosts,
                Categories = Categories,
            };
        }
    }
}