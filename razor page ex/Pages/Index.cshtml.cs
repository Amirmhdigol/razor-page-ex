using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using RazorEX.BAL.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace razor_page_ex.Pages
{

    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IPost _postservice;
        public IndexModel(ILogger<IndexModel> logger, IPost postservice)
        {
            _logger = logger;
            _postservice = postservice;
        }

        public void OnGet()
        {

        }

        public IActionResult OnGetPopularPosts()
        {
            return Partial("_PopularPosts", _postservice.GetPopularPosts());
        }
    }

}
