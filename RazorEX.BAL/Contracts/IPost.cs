using RazorEX.BAL.DTOs;
using RazorEX.BAL.DTOs.PostDTO;
using RazorEX.BAL.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorEX.BAL.Contracts
{
    public interface IPost
    {
        OperationResult CreatePost(CreatePostDTO command);
        OperationResult EditPost(EditPostDTO commmand);
        PostDTO GetPostById(int postId);
        //PostFilterDto GetPostsByFilter(PostFilterParams filterParams);
        bool IsSlugExist(string slug);
        PostFilterDTO GetPostByFilter(PostFilterParams postFilterParams);
    }
}

