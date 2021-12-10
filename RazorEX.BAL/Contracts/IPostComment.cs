using RazorEX.BAL.DTOs.PostCommentsDTO;
using RazorEX.BAL.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorEX.BAL.Contracts
{
    public interface IPostComment
    {
        OperationResult CreatePostComment(CreatePostCommentDTO createPostCommentDTO);
        List<PostCommentDTO> GetPostComments(int Postid);
    }
}
