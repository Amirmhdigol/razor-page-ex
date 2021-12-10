using Microsoft.EntityFrameworkCore;
using RazorEx.DAL.Context;
using RazorEx.DAL.Entities;
using RazorEX.BAL.Contracts;
using RazorEX.BAL.DTOs.PostCommentsDTO;
using RazorEX.BAL.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorEX.BAL.Services
{
    public class PostComments : IPostComment
    {
        private readonly RXContext _context;

        public PostComments(RXContext context)
        {
            _context = context;
        }

        public OperationResult CreatePostComment(CreatePostCommentDTO createPostCommentDTO)
        {
            var Comment = new PostComment()
            {
                PostId = createPostCommentDTO.PostId,
                UserId = createPostCommentDTO.UserId,
                Text = createPostCommentDTO.Text
            };
            _context.Add(Comment);
            _context.SaveChanges();
            return OperationResult.Success();
        }

        public List<PostCommentDTO> GetPostComments(int Postid)
        {
            return _context.PostComments.Include(a=>a.User)
                .Where(a => a.PostId == Postid)
                .Select(Comment => new PostCommentDTO()
                {
                    PostCommentId = Comment.Id,
                    PostId = Comment.PostId,
                    UserFullName = Comment.User.UserName,
                    Text = Comment.Text,    
                    CreationDate = Comment.CreationDate
                }).ToList();
        }
    }
}
