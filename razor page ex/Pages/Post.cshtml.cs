using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorEX.BAL.Contracts;
using RazorEX.BAL.DTOs.PostCommentsDTO;
using RazorEX.BAL.DTOs.PostDTO;
using RazorEX.BAL.Utilities;

namespace razor_page_ex.Pages
{
    public class PostModel : PageModel
    {
        private readonly IPost _post;
        private readonly IPostComment _postComment;

        public PostModel(IPost post, IPostComment postComment)
        {
            _post = post;
            _postComment = postComment;
        }

        public PostDTO Post { get; set; }
        [Required]
        [BindProperty]
        public string Text { get; set; }
        [BindProperty]        
        public int PostId { get; set; }

        public List<PostCommentDTO> PostComments { get; set; }
        public List<PostDTO> RelatedPosts { get; set; }

        public IActionResult OnGet(string slug)
        {
            Post = _post.GetPostBySlug(slug);

            if (Post == null)
                return NotFound();

            PostComments = _postComment.GetPostComments(Post.PostId);
            RelatedPosts = _post.GetRelatedPosts(Post.SubCategoryId ?? Post.CategoryId);

            _post.IncreaseVisit(Post.PostId);

            return Page();
        }
        
        public IActionResult OnPost(string slug)
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToPage("Post", new { slug });

            if (!ModelState.IsValid)
            {
                Post = _post.GetPostBySlug(slug);
                PostComments = _postComment.GetPostComments(Post.PostId);
                RelatedPosts = _post.GetRelatedPosts(Post.SubCategoryId ?? Post.CategoryId);
                return Page();
            }
            _postComment.CreatePostComment(new CreatePostCommentDTO()
            {
                PostId = PostId,
                Text = Text,
                UserId = User.Getid()
            });

            return RedirectToPage("Post", new { slug });
        }
    }
}
