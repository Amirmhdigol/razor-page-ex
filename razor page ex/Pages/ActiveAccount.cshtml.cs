using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorEX.BAL.Contracts;
using System.Web.Mvc;

namespace razor_page_ex.Pages.Register
{
    public class ActiveAccountModel : PageModel
    {
        private readonly ISignup _signup;

        public ActiveAccountModel(ISignup signup)
        {
            _signup = signup;
        }

        public void OnGet()
        {
            
        }
        public bool ActiveA { get; set; }
        public IActionResult OnGetActiveAccount(string id)
        {
            ActiveA = _signup.ActiveAccount(id);
            return Page();
        }
    }
}
