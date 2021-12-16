using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorEX.BAL.Contracts;
using RazorEX.BAL.DTOs.PostDTO;

namespace razor_page_ex.Pages
{
    public class SearchPageModel : PageModel
    {
        private readonly IPost _post;

        public SearchPageModel(IPost post)
        {
            _post = post;
        }

        public PostFilterDTO Posts { get; set; }

        public void OnGet(string CategorySlug = null, int PageId = 1, string q = null)
        {
            Posts = _post.GetPostByFilter(new PostFilterParams()
            {
                CategorySlug = CategorySlug,
                PageId = PageId,
                Title = q,
                Take = 4
            });
        }

        public IActionResult OnGetPagination(int pageId = 1, string categorySlug = null, string q = null)
        {
            var model = _post.GetPostByFilter(new PostFilterParams()
            {
                CategorySlug = categorySlug,
                PageId = pageId,
                Take = 4,
                Title = q
            });
            return Partial("_SearchView", model);
        }
    }
}
