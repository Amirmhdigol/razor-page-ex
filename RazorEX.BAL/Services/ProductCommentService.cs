using Microsoft.EntityFrameworkCore;
using RazorEx.DAL.Context;
using RazorEx.DAL.Entities;
using RazorEX.BAL.Contracts;
using RazorEX.BAL.DTOs.ProductCommentDTOs;
using RazorEX.BAL.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorEX.BAL.Services
{
    public class ProductCommentService : IProductComment
    {
        private readonly RXContext _RXContext;

        public ProductCommentService(RXContext rXContext)
        {
            _RXContext = rXContext;
        }

        public OperationResult CreateProductComment(CreateProductCommentDTO createProductCommentDTO)
        {
            var Comment = new ProductComment()
            {
                ProductId = createProductCommentDTO.ProductId,
                UserId = createProductCommentDTO.UserId,
                Text = createProductCommentDTO.Text
            };
            _RXContext.Add(Comment);
            _RXContext.SaveChanges();
            return OperationResult.Success();
        }

        public List<ProductCommentDTO> GetProductComments(int ProductId)
        {
            return _RXContext.ProductComments.Include(a => a.User)
                .Where(a => a.ProductId == ProductId)
                .Select(Comment => new ProductCommentDTO()
                {
                    ProductCommentId = Comment.Id,
                    ProductId = Comment.ProductId,
                    UserFullName = Comment.User.UserName,
                    Text = Comment.Text,
                    CreationDate = Comment.CreationDate
                }).ToList();
        }
    }
}
