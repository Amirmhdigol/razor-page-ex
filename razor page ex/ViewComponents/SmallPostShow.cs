using Microsoft.AspNetCore.Mvc;
using RazorEX.BAL.Contracts;
using System.Linq;
using System.Threading.Tasks;

namespace razor_page_ex.ViewComponents
{
    public class SmallPostShow : ViewComponent
    {
        private readonly IMainPage _mainpage;

        public SmallPostShow(IMainPage mainpage)
        {
            _mainpage = mainpage;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var Posts = _mainpage.GetData();
            var bigPost = Posts.SpecialPosts.First();
            var SmallPosts = Posts.SpecialPosts.Where(r => r.PostId != bigPost.PostId);

            return await Task.FromResult((IViewComponentResult)View("SmallPosts", Posts));
        }
    }
}
