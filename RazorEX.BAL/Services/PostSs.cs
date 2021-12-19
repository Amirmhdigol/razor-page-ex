using Microsoft.EntityFrameworkCore;
using RazorEx.DAL.Context;
using RazorEx.DAL.Entities;
using RazorEX.BAL.Contracts;
using RazorEX.BAL.DTOs.PostCommentsDTO;
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

            Post.ImageName = _fileManager.SaveFile2(command.ImageFile, Directories.Post);
            _rXContext.Posts.Add(Post);
            _rXContext.SaveChanges();
            return OperationResult.Success();
        }

        public OperationResult EditPost(EditPostDTO command)
        {
            var FindedPost = _rXContext.Posts.FirstOrDefault(a => a.Id == command.PostId);

            var oldImage = FindedPost.ImageName;
            var newSlug = command.Slug.ToSlug();

            if (FindedPost.Slug != newSlug)
                if (IsSlugExist(newSlug))
                    return OperationResult.Error("Slug تکراری است");

            if (FindedPost == null)
                return OperationResult.NotFound();

            FindedPost.Description = command.Description;
            FindedPost.Title = command.Title;
            FindedPost.CategoryId = command.CategoryId;
            FindedPost.Slug = command.Slug.ToSlug();
            FindedPost.SubCategoryId = command.SubCategoryId;
            FindedPost.IsSpecial = command.IsSpecial;

            if (command.ImageFile != null)
                FindedPost.ImageName = _fileManager.SaveFile2(command.ImageFile, Directories.Post);

            _rXContext.Posts.Update(FindedPost);
            _rXContext.SaveChanges();
            if (command.ImageFile != null)
                _fileManager.DeleteFile(oldImage, Directories.Post);

            return OperationResult.Success();
        }

        public PostDTO GetPostBySlug(string slug)
        {
            var FindedPost = _rXContext.Posts
              .Include(c => c.SubCategory)
              .Include(c => c.Category)
              .Include(v => v.User)
              .FirstOrDefault(c => c.Slug == slug);

            if (FindedPost == null)
                return null;

            return FindPostMapper.Map(FindedPost);
        }

        public PostFilterDTO GetPostByFilter(PostFilterParams postFilterParams)
        {
            var Result = _rXContext.Posts
                .Include(n => n.Category)
                .Include(a => a.SubCategory)
                .OrderByDescending(p => p.CreationDate)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(postFilterParams.CategorySlug))
                Result = Result.Where(a => a.Category.Slug == postFilterParams.CategorySlug 
                || a.SubCategory.Slug == postFilterParams.CategorySlug);

            if (!string.IsNullOrWhiteSpace(postFilterParams.Title))
                Result = Result.Where(a => a.Title.Contains(postFilterParams.Title));

            var skip = (postFilterParams.PageId - 1) * postFilterParams.Take;
            var model = new PostFilterDTO()
            {
                Posts = Result.Skip(skip).Take(postFilterParams.Take)
                                .Select(post => FindPostMapper.Map(post)).ToList(),
                FilterParams = postFilterParams,
            };
            model.GeneratePaging(Result, postFilterParams.Take, postFilterParams.PageId);

            return model;
        }

        public PostDTO GetPostById(int postId)
        {
            var FindedPost = _rXContext.Posts
                .Include(c => c.SubCategory)
                .Include(c => c.Category)
                .FirstOrDefault(c => c.Id == postId); ;

            return FindPostMapper.Map(FindedPost);
        }

        public bool IsSlugExist(string slug)
        {
            return _rXContext.Posts.Any(p => p.Slug == slug.ToSlug());
        }

        public List<PostDTO> GetRelatedPosts(int CategotyId)
        {
            var FindedPosts = _rXContext.Posts
                .Where(a => a.CategoryId == CategotyId || a.SubCategoryId == CategotyId)
                .OrderByDescending(a => a.CreationDate)
                .Take(6)
                .Select(a => FindPostMapper.Map(a)).ToList();

            return FindedPosts;
        }

        public List<PostDTO> GetPopularPosts()
        {
            var FindedPosts = _rXContext.Posts
                .Include(s => s.User)
                .OrderByDescending(a => a.Visit)
                .Take(4)
                .Select(posts => FindPostMapper.Map(posts)).ToList();

            return FindedPosts;
        }

        public OperationResult DeletePost(int Id)
        {
            var FindedPost = _rXContext.Posts
                .Include(c => c.SubCategory)
                .Include(c => c.Category)
                .FirstOrDefault(c => c.Id == Id);

            _rXContext.Posts.Remove(FindedPost);
            _rXContext.SaveChanges();

            return OperationResult.Success();
        }

        public void IncreaseVisit(int PostId)
        {
            var post = _rXContext.Posts.FirstOrDefault(a => a.Id == PostId);
            post.Visit += 1;
            _rXContext.SaveChanges();
        }
    }
}
