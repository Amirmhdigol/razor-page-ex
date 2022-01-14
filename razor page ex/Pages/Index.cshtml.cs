using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using RazorEX.BAL.Contracts;
using RazorEX.BAL.DTOs.MainPageDTO;
using RazorEX.BAL.DTOs.PostDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace razor_page_ex.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IPost _postservice;
        private readonly IMainPage _mainpage;

        public IndexModel(ILogger<IndexModel> logger, IPost postservice, IMainPage mainpage)
        {
            _logger = logger;
            _postservice = postservice;
            _mainpage = mainpage;
        }
        public PageDTO MainPageData { get; set; }

        public void OnGet()
        {
            //throw new Exception();
            MainPageData = _mainpage.GetData();
        }
        
        public IActionResult OnGetLatestPosts(string categorySlug)
        {
            var filterDto = _postservice.GetPostByFilter(new PostFilterParams()
            {
                CategorySlug = categorySlug,
                PageId = 1,
                Take = 6
            });
            return Partial("_LatestPosts", filterDto.Posts);
        }

        public IActionResult OnGetPopularPosts()
        {
            return Partial("_PopularPosts", _postservice.GetPopularPosts());
        }
    }
}
