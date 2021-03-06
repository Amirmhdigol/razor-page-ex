using RazorEX.BAL.DTOs;
using RazorEX.BAL.DTOs.PostCommentsDTO;
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
        OperationResult DeletePost(int Id);
        PostDTO GetPostById(int postId);
        PostDTO GetPostBySlug(string slug);
        PostFilterDTO GetPostByFilter(PostFilterParams filterParams);
        bool IsSlugExist(string slug);
        List<PostDTO> GetRelatedPosts(int CategotyId);
        List<PostDTO> GetPopularPosts();
        void IncreaseVisit(int PostId);
    }
}

