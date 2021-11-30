using Microsoft.EntityFrameworkCore;
using RazorEx.DAL.Context;
using RazorEx.DAL.Entities;
using RazorEX.BAL.Contracts;
using RazorEX.BAL.DTOs.PostDTO;
using RazorEX.BAL.Utilities;
using RazorEX.BAL.Utilities.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace RazorEX.BAL.Services
{
    public class PostSs : IPost
    {
        private readonly IFileManager _fileManager;
        private readonly RXContext _rXContext;

        public PostSs(RXContext rXContext, IFileManager fileManager)
        {
            _fileManager = fileManager;
            _rXContext = rXContext;
        }

        public OperationResult CreatePost(CreatePostDTO command)
        {
            if (command.ImageFile == null)
                return OperationResult.Error();

            _rXContext.Posts.Include(a => a.Category).Include(b => b.SubCategory);

            var Post = CreatePostMapper.Map(command);

            if (IsSlugExist(Post.Slug)) 
                return OperationResult.Error("Slug تکراری است");

            Post.ImageName = _fileManager.SaveFile(command.ImageFile, Directories.Post);
            _rXContext.Posts.Add(Post);
            _rXContext.SaveChanges();
            return OperationResult.Success();
        }

        public OperationResult EditPost(EditPostDTO command)
        {
            var FindedPost = _rXContext.Posts.FirstOrDefault(a => a.Id == command.PostId);
            if (FindedPost == null)
                return OperationResult.NotFound();

            FindedPost.Description = command.Description;
            FindedPost.Title = command.Title;
            FindedPost.CategoryId = command.CategoryId;
            FindedPost.Slug = command.Slug.ToSlug();

            _rXContext.Posts.Update(FindedPost);
            return OperationResult.Success();
        }

        public PostFilterDTO GetPostByFilter(PostFilterParams postFilterParams)
        {
            var Result = _rXContext.Posts.Include(n => n.Category).Include(a => a.SubCategory)
                .OrderBy(p => p.CreationDate).AsQueryable();

            if (!string.IsNullOrWhiteSpace(postFilterParams.CategorySlug))
                Result = Result.Where(a => a.Category.Slug == postFilterParams.CategorySlug);

            if (!string.IsNullOrWhiteSpace(postFilterParams.Title))
                Result = Result.Where(a => a.Title.Contains(postFilterParams.Title));

            var skip = (postFilterParams.PageId - 1) * postFilterParams.Take;
            var pagecount = Result.Count() / postFilterParams.Take;

            return new PostFilterDTO()
            {
                Posts = Result.Skip(skip).Take(postFilterParams.Take).Select(post => FindPostMapper.Map(post)).ToList(),
                FilterParams = postFilterParams,
                PageCount = pagecount
            };
        }

        public PostDTO GetPostById(int postId)
        {
            var FindedPost = _rXContext.Posts
                .Include(c => c.SubCategory)
                .Include(c => c.Category)
                .FirstOrDefault(c => c.Id == postId); ;

            if (FindedPost == null)
                return null;

            return FindPostMapper.Map(FindedPost);
        }

        public bool IsSlugExist(string slug)
        {
            return _rXContext.Posts.Any(p => p.Slug == slug.ToSlug());
        }
    }
}
