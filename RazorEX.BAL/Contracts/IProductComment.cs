using RazorEX.BAL.DTOs.ProductCommentDTOs;
using RazorEX.BAL.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorEX.BAL.Contracts
{
    public interface IProductComment
    {
        OperationResult CreateProductComment(CreateProductCommentDTO createProductCommentDTO);
        List<ProductCommentDTO> GetProductComments(int Postid);
    }
}
